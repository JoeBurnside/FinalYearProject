using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS
{
    public partial class Buttons : Form
    {
        DataTable datatable = new DataTable();
        List<ListObject> theList = new List<ListObject>();
        public Buttons()
        {
            InitializeComponent();
        }

        private void Buttons_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            Filldatatable(Globals.MenuNo);
            popbuttons();
            fillcombo();
            comboBoxMenu.SelectedIndex = 0;
            comboBoxMenu.SelectedValueChanged += ComboBoxMenu_SelectedValueChanged;
        }
        private void fillcombo()
        {
            comboBoxMenu.DataSource = null;
            comboBoxMenu.Items.Clear();
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
            comboBoxMenu.DataSource = theList;
            comboBoxMenu.DisplayMember = "Name";
            comboBoxMenu.ValueMember = "ID";
        }
        private void ComboBoxMenu_SelectedValueChanged(object sender, EventArgs e)
        {
            Globals.MenuNo = int.Parse(comboBoxMenu.SelectedValue.ToString());
            Filldatatable(Globals.MenuNo);
            popbuttons();
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
        private void Filldatatable(int menuid){
            datatable.Clear();
            SqlConnection table = new SqlConnection(Globals.dataconnection);
            SqlCommand fill = new SqlCommand("SELECT * FROM Button WHERE MenuID = @id ORDER BY ButtonID", table);
            fill.Parameters.AddWithValue("@id", Globals.MenuNo);
            table.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = fill;
            da.Fill(datatable);
            table.Close();
        }
        private void popbuttons()
        {

            int i = 1;
            while (i != 57)
            {
                string butname = "button";
                butname += i.ToString();
                Button control = (Button)this.Controls.Find(butname, true).FirstOrDefault();
                control.BackColor = Color.FromName(datatable.Rows[i - 1][2].ToString());
                control.ForeColor = Color.FromName(datatable.Rows[i - 1][5].ToString());
                control.Font = new Font(datatable.Rows[i - 1][3].ToString(), (int)datatable.Rows[i - 1][4]);
                if (datatable.Rows[i - 1][6].ToString() != "Y")
                {
                    control.Text = butname;
                }
                else
                {
                    switch (datatable.Rows[i - 1][7].ToString())
                    {
                        case "Product":
                            SqlConnection buttonname = new SqlConnection(Globals.dataconnection);
                            SqlCommand shownames = new SqlCommand("SELECT Product.Name, Unit.Name FROM Product, Unit, SaleUnit WHERE Product.ProductID = SaleUnit.ProductID AND Unit.UnitID = SaleUnit.UnitID AND SaleUnitID = @id", buttonname);
                            shownames.Parameters.AddWithValue("@id", (int)datatable.Rows[i - 1][8]);
                            buttonname.Open();
                            SqlDataReader reader = shownames.ExecuteReader();
                            while (reader.Read())
                            {
                                control.Text = reader.GetString(0) + Environment.NewLine + reader.GetString(1);
                            }
                            buttonname.Close();
                            break;
                        case "Menu":
                            SqlConnection btnname = new SqlConnection(Globals.dataconnection);
                            SqlCommand showname = new SqlCommand("SELECT Name FROM Menu WHERE MenuID = @id", btnname);
                            showname.Parameters.AddWithValue("@id", (int)datatable.Rows[i - 1][9]);
                            btnname.Open();
                            SqlDataReader reader2 = showname.ExecuteReader();
                            while (reader2.Read())
                            {
                                control.Text = reader2.GetString(0);
                            }
                            btnname.Close();
                            break;
                    }
                    
                }
                i++;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void filllist()
        {
            comboBoxMenu.DataSource = null;
            comboBoxMenu.Items.Clear();
            theList.Clear();
            SqlConnection menu = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT MenuID, Name FROM Menu", menu);
            menu.Open();
            show.Parameters.AddWithValue("@id", Globals.IDNo);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList.Add(new ListObject(reader.GetString(1), reader.GetInt32(0)));
            }
            menu.Close();
            comboBoxMenu.DataSource = theList;
            comboBoxMenu.DisplayMember = "Name";
            comboBoxMenu.ValueMember = "ID";
            comboBoxMenu.SelectedIndex = 0;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddMenu addmenu = new AddMenu();
            addmenu.ShowDialog();
            Filldatatable(Globals.MenuNo);
            fillcombo();
            popbuttons();
        }
        private void EditButton(int buttonno)
        {
            Globals.ButtonNo = buttonno;
            EditButton editbutton = new EditButton();
            editbutton.ShowDialog();
            Filldatatable(Globals.MenuNo);
            popbuttons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditButton(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditButton(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditButton(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditButton(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditButton(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditButton(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EditButton(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            EditButton(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditButton(9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            EditButton(10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            EditButton(11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EditButton(12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            EditButton(13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            EditButton(14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            EditButton(15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            EditButton(16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            EditButton(17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            EditButton(18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            EditButton(19);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            EditButton(20);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            EditButton(21);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            EditButton(22);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            EditButton(23);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            EditButton(24);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            EditButton(25);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            EditButton(26);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            EditButton(27);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            EditButton(28);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            EditButton(29);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            EditButton(30);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            EditButton(31);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            EditButton(32);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            EditButton(33);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            EditButton(34);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            EditButton(35);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            EditButton(36);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            EditButton(37);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            EditButton(38);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            EditButton(39);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            EditButton(40);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            EditButton(41);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            EditButton(42);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            EditButton(43);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            EditButton(44);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            EditButton(45);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            EditButton(46);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            EditButton(47);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            EditButton(48);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            EditButton(49);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            EditButton(50);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            EditButton(51);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            EditButton(52);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            EditButton(53);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            EditButton(54);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            EditButton(55);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            EditButton(56);
        }
    }
}
