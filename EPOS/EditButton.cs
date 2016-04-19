using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS
{
    public partial class EditButton : Form
    {
        List<ListObject> theList = new List<ListObject>();
        string Type;
        public EditButton()
        {
            InitializeComponent();
        }

        private void EditButton_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            fillboxes();
            SetFocus();
            setvariable();
            focusvariable();
            comboBoxBtnColour.SelectedValueChanged += ComboBoxBtn_SelectedValueChanged;
            comboBoxFont.SelectedValueChanged += ComboBoxBtn_SelectedValueChanged;
            comboBoxFontSize.SelectedValueChanged += ComboBoxBtn_SelectedValueChanged;
            comboBoxFontColour.SelectedValueChanged += ComboBoxBtn_SelectedValueChanged;
            comboBoxType.SelectedValueChanged += ComboBoxType_SelectedValueChanged;
            comboBoxVariable.SelectedValueChanged += ComboBoxBtn_SelectedValueChanged;
            Preview();
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }
        private void fillboxes()
        {
            comboBoxBtnColour.DrawItem += new DrawItemEventHandler(comboBoxBtnColour_DrawItem);
            comboBoxFontColour.DrawItem += new DrawItemEventHandler(comboBoxFontColour_DrawItem);
            comboBoxFont.DrawItem += new DrawItemEventHandler(comboBoxFont_DrawItem);
            Type t = typeof(Color);
            PropertyInfo[] p = t.GetProperties();

            foreach (PropertyInfo item in p)
            {
                if (item.PropertyType.FullName.Equals("System.Drawing.Color", StringComparison.CurrentCultureIgnoreCase))
                {
                    comboBoxBtnColour.Items.Add(item.Name);
                    comboBoxFontColour.Items.Add(item.Name);
                }
            }
            InstalledFontCollection inst = new InstalledFontCollection();
            foreach (FontFamily fnt in inst.Families)
            {
                comboBoxFont.Items.Add(fnt.Name);
            }


        }
        private void comboBoxBtnColour_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = comboBoxBtnColour.Font;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 350, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }
        private void comboBoxFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font(((ComboBox)sender).Items[e.Index].ToString(), 18); ;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black,e.Bounds);
            }
        }
        private void comboBoxFontColour_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = comboBoxFontColour.Font;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 350, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }
        private void Preview()
        {
            button1.BackColor = Color.FromName(comboBoxBtnColour.SelectedItem.ToString());
            button1.ForeColor = Color.FromName(comboBoxFontColour.SelectedItem.ToString());
            button1.Font = new Font(comboBoxFont.SelectedItem.ToString(), int.Parse(comboBoxFontSize.SelectedItem.ToString()));
            if (comboBoxType.SelectedIndex != 1)
            {
                SqlConnection buttonname = new SqlConnection(Globals.dataconnection);
                SqlCommand shownames = new SqlCommand("SELECT Product.Name, Unit.Name FROM Product, Unit, SaleUnit WHERE Product.ProductID = SaleUnit.ProductID AND Unit.UnitID = SaleUnit.UnitID AND SaleUnitID = @id", buttonname);
                shownames.Parameters.AddWithValue("@id", int.Parse(comboBoxVariable.SelectedValue.ToString()));
                buttonname.Open();
                SqlDataReader reader = shownames.ExecuteReader();
                while (reader.Read())
                {
                    button1.Text = reader.GetString(0) + Environment.NewLine + reader.GetString(1);
                }
                buttonname.Close();
            }
            else
            {
                SqlConnection buttonname = new SqlConnection(Globals.dataconnection);
                SqlCommand shownames = new SqlCommand("SELECT Name FROM Menu WHERE MenuID = @id", buttonname);
                shownames.Parameters.AddWithValue("@id", int.Parse(comboBoxVariable.SelectedValue.ToString()));
                buttonname.Open();
                SqlDataReader reader = shownames.ExecuteReader();
                while (reader.Read())
                {
                    button1.Text = reader.GetString(0);
                }
                buttonname.Close();
            }
        }
        private void SetFocus()
        {
            SqlConnection buttonname = new SqlConnection(Globals.dataconnection);
            SqlCommand shownames = new SqlCommand("SELECT Color, Font, FontSize, FontColor, Type FROM Button WHERE MenuID = @menu AND ButtonID = @button", buttonname);
            shownames.Parameters.AddWithValue("@menu", Globals.MenuNo);
            shownames.Parameters.AddWithValue("@button", Globals.ButtonNo);
            buttonname.Open();
            SqlDataReader reader = shownames.ExecuteReader();
            while (reader.Read())
            {
                comboBoxBtnColour.SelectedItem = reader.GetString(0);
                comboBoxFont.SelectedItem = reader.GetString(1);
                comboBoxFontSize.SelectedItem = reader.GetInt32(2).ToString();
                comboBoxFontColour.SelectedItem = reader.GetString(3);
                Type = reader.GetString(4);
                if (Type != "Menu")
                {
                    comboBoxType.SelectedIndex = 0;
                }
                else
                {
                    comboBoxType.SelectedIndex = 1;
                }
            }
            buttonname.Close();
        }
        private void setvariable()
        {
            if (comboBoxType.SelectedIndex == 1)
            {
                labelVariable.Text = "Menu";
                comboBoxVariable.DataSource = null;
                comboBoxVariable.Items.Clear();
                theList.Clear();
                SqlConnection unit = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Name, MenuID FROM Menu ORDER BY MenuID", unit);
                unit.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    theList.Add(new ListObject(reader.GetString(0), reader.GetInt32(1)));
                }
                unit.Close();
                comboBoxVariable.DataSource = theList;
                comboBoxVariable.DisplayMember = "Name";
                comboBoxVariable.ValueMember = "ID";
                comboBoxVariable.SelectedIndex = 0;
            }
            else
            {
                labelVariable.Text = "Product";
                comboBoxVariable.DataSource = null;
                comboBoxVariable.Items.Clear();
                theList.Clear();
                SqlConnection unit = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT SaleUnit.SaleUnitID, Product.Name, Unit.Name FROM Unit, SaleUnit, Product WHERE Unit.UnitID = SaleUnit.UnitID AND SaleUnit.ProductID = Product.ProductID  ORDER BY Product.Name", unit);
                unit.Open();
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    theList.Add(new ListObject(reader.GetString(1) + " " + reader.GetString(2), reader.GetInt32(0)));
                }
                unit.Close();
                comboBoxVariable.DataSource = theList;
                comboBoxVariable.DisplayMember = "Name";
                comboBoxVariable.ValueMember = "ID";
                comboBoxVariable.SelectedIndex = 0;
            }
        }
        private void focusvariable()
        {
            switch (Type)
            {
                case "Product":
                    SqlConnection unit = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("SELECT SaleUnitID FROM Button WHERE MenuID = @menu AND ButtonID = @button", unit);
                    unit.Open();
                    show.Parameters.AddWithValue("@menu", Globals.MenuNo);
                    show.Parameters.AddWithValue("@button", Globals.ButtonNo);
                    SqlDataReader reader = show.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBoxVariable.SelectedValue = reader.GetInt32(0);
                    }
                    unit.Close();
                    break;
                case "Menu":
                    SqlConnection unit2 = new SqlConnection(Globals.dataconnection);
                    SqlCommand show2 = new SqlCommand("SELECT MenuLinkID FROM Button WHERE MenuID = @menu AND ButtonID = @button", unit2);
                    unit2.Open();
                    show2.Parameters.AddWithValue("@menu", Globals.MenuNo);
                    show2.Parameters.AddWithValue("@button", Globals.ButtonNo);
                    SqlDataReader reader2 = show2.ExecuteReader();
                    while (reader2.Read())
                    {
                        comboBoxVariable.SelectedValue = reader2.GetInt32(0);
                    }
                    unit2.Close();
                    break;
                case "Blank":
                    comboBoxVariable.SelectedIndex = 0;
                    break;
            }
                
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ComboBoxBtn_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxVariable.SelectedValue != null)
            {
            Preview();
            }

        }
        private void ComboBoxType_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxVariable.SelectedIndex = 0;
            setvariable();
            Preview();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to delete this button?",
                      "Delete?", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    SqlConnection button = new SqlConnection(Globals.dataconnection);
                    SqlCommand insert = new SqlCommand("UPDATE Button SET Color = @color, Font = @font, FontSize = @fontsize, FontColor = @fontcolor, Active = @active, Type = @type, SaleUnitID = NULL, MenuLinkID = NULL WHERE MenuID = @menuid AND ButtonID = @buttonid", button);
                    button.Open();
                    insert.Parameters.AddWithValue("@color", "Silver");
                    insert.Parameters.AddWithValue("@font", "Microsoft Sans Serif");
                    insert.Parameters.AddWithValue("@fontsize", 10);
                    insert.Parameters.AddWithValue("@fontcolor", "Black");
                    insert.Parameters.AddWithValue("@active", "N");
                    insert.Parameters.AddWithValue("@type", "Blank");
                    insert.Parameters.AddWithValue("@menuid", Globals.MenuNo);
                    insert.Parameters.AddWithValue("@buttonid", Globals.ButtonNo);
                    insert.ExecuteNonQuery();
                    button.Close();
                    this.Close();
                    break;
                case DialogResult.No: break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex != 1)
            {
                SqlConnection button = new SqlConnection(Globals.dataconnection);
                SqlCommand insert = new SqlCommand("UPDATE Button SET Color = @color, Font = @font, FontSize = @fontsize, FontColor = @fontcolor, Active = @active, Type = @type, SaleUnitID = @saleunit, MenuLinkID = NULL WHERE MenuID = @menuid AND ButtonID = @buttonid", button);
                button.Open();
                insert.Parameters.AddWithValue("@color", comboBoxBtnColour.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@font", comboBoxFont.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@fontsize", int.Parse(comboBoxFontSize.SelectedItem.ToString()));
                insert.Parameters.AddWithValue("@fontcolor", comboBoxFontColour.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@active", "Y");
                insert.Parameters.AddWithValue("@type", "Product");
                insert.Parameters.AddWithValue("@saleunit", int.Parse(comboBoxVariable.SelectedValue.ToString()));
                insert.Parameters.AddWithValue("@menuid", Globals.MenuNo);
                insert.Parameters.AddWithValue("@buttonid", Globals.ButtonNo);
                insert.ExecuteNonQuery();
                button.Close();
            }
            else
            {
                SqlConnection button = new SqlConnection(Globals.dataconnection);
                SqlCommand insert = new SqlCommand("UPDATE Button SET Color = @color, Font = @font, FontSize = @fontsize, FontColor = @fontcolor, Active = @active, Type = @type, SaleUnitID = NULL, MenuLinkID = @menulink WHERE MenuID = @menuid AND ButtonID = @buttonid", button);
                button.Open();
                insert.Parameters.AddWithValue("@color", comboBoxBtnColour.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@font", comboBoxFont.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@fontsize", int.Parse(comboBoxFontSize.SelectedItem.ToString()));
                insert.Parameters.AddWithValue("@fontcolor", comboBoxFontColour.SelectedItem.ToString());
                insert.Parameters.AddWithValue("@active", "Y");
                insert.Parameters.AddWithValue("@type", "Menu");
                insert.Parameters.AddWithValue("@menulink", int.Parse(comboBoxVariable.SelectedValue.ToString()));
                insert.Parameters.AddWithValue("@menuid", Globals.MenuNo);
                insert.Parameters.AddWithValue("@buttonid", Globals.ButtonNo);
                insert.ExecuteNonQuery();
                button.Close();
            }
            this.Close();
        }
    }
}
