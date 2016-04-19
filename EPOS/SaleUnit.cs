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
    public partial class SaleUnit : Form
    {
        List<ListObject> theList = new List<ListObject>();
        List<ListObject> theList2 = new List<ListObject>();
        public SaleUnit()
        {
            InitializeComponent();
        }

       private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
       {
       }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            ApplyDiscount apply = new ApplyDiscount();
            apply.ShowDialog();
            this.Close();
        }

        private void SaleUnit_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            filllist();
            fillbox();
            listBox1.SelectedValueChanged += new EventHandler(Listbox1_SelectedValueChanged);
            textBoxPrice.Click += new EventHandler(textBoxPrice_Click);
        }
        private void textBoxPrice_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxPrice.Text;
            Numberpad numpad = new Numberpad();
            numpad.ShowDialog();
            this.ActiveControl = null;
            textBoxPrice.Text = Globals.KeyboardString;
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
        private void fillbox()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            theList.Clear();
            SqlConnection salesunit = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT SaleUnitID, Price, Name FROM SaleUnit, Unit WHERE Unit.UnitID = SaleUnit.UnitID AND ProductID = @id ORDER BY Name", salesunit);
            salesunit.Open();
            show.Parameters.AddWithValue("@id", Globals.IDNo);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList.Add(new ListObject(reader.GetString(2)+" - £"+reader.GetDecimal(1).ToString() , reader.GetInt32(0)));
            }
            salesunit.Close();
            listBox1.DataSource = theList;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
            listBox1.SelectedIndex = -1;
        }
        private void filllist()
        {
            comboBoxUnit.DataSource = null;
            comboBoxUnit.Items.Clear();
            theList2.Clear();
            SqlConnection unit = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT UnitID, Name FROM Unit ORDER BY Name", unit);
            unit.Open();
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList2.Add(new ListObject(reader.GetString(1), reader.GetInt32(0)));
            }
            unit.Close();
            comboBoxUnit.DataSource = theList2;
            comboBoxUnit.DisplayMember = "Name";
            comboBoxUnit.ValueMember = "ID";
            comboBoxUnit.SelectedIndex = 0;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this selling unit?",
                      "Delete?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        try {
                            int id = int.Parse(listBox1.SelectedValue.ToString());
                            SqlConnection un = new SqlConnection(Globals.dataconnection);
                            un.Open();
                            SqlCommand delete = new SqlCommand("DELETE FROM SaleUnit WHERE SaleUnitID = @id", un);
                            delete.Parameters.AddWithValue("@id", id);
                            delete.ExecuteNonQuery();
                            un.Close();
                        }
                        catch
                        {
                            MessageBox.Show("This selling unit can not be deleted as it is currently in use");
                        }
                        break;
                    case DialogResult.No: break;
                }

            }
            else {
                MessageBox.Show("Please select a category from the list");
            }
            fillbox();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (textBoxPrice.Text != "")
                {
                    decimal price = Decimal.Parse(textBoxPrice.Text);
                    decimal pricernd = Decimal.Round(price, 2);
                    SqlConnection prod = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE SaleUnit SET UnitID = @unit, Price = @price WHERE SaleUnitID = @id", prod);
                    prod.Open();
                    show.Parameters.AddWithValue("@unit", int.Parse(comboBoxUnit.SelectedValue.ToString()));
                    show.Parameters.AddWithValue("@price", pricernd);
                    show.Parameters.AddWithValue("@id", int.Parse(listBox1.SelectedValue.ToString()));
                    show.ExecuteNonQuery();
                    prod.Close();
                    fillbox();
                }
                else
                {
                    MessageBox.Show("Please enter a price");
                }
            }
            else {
                MessageBox.Show("Please select a product from the list");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxPrice.Text != "")
            {
                decimal price;
                if (decimal.TryParse(textBoxPrice.Text, out price))
                {
                    decimal pricernd = Decimal.Round(price, 2);
                    SqlConnection adduni = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO SaleUnit (ProductID, UnitID, Price) VALUES (@prodid, @unitid, @price)", adduni);
                    adduni.Open();
                    show.Parameters.AddWithValue("@prodid", Globals.IDNo);
                    show.Parameters.AddWithValue("@unitid", int.Parse(comboBoxUnit.SelectedValue.ToString()));
                    show.Parameters.AddWithValue("@price", pricernd);
                    show.ExecuteNonQuery();
                    adduni.Close();
                    fillbox();
                    textBoxPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Price format should be £0.00");
                }
            }
            else
            {
                MessageBox.Show("Please enter a price");
            }
        }
        private void Listbox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                SqlConnection salesu = new SqlConnection(Globals.dataconnection);
                SqlCommand show1 = new SqlCommand("SELECT SaleUnit.UnitID, Price FROM SaleUnit, Unit WHERE SaleUnitID = @id ORDER BY Name", salesu);
                salesu.Open();
                show1.Parameters.AddWithValue("@id", int.Parse(listBox1.SelectedValue.ToString()));
                SqlDataReader reader = show1.ExecuteReader();
                while (reader.Read())
                {
                    textBoxPrice.Text = reader.GetDecimal(1).ToString();
                    comboBoxUnit.SelectedValue = reader.GetInt32(0);
                }
                salesu.Close();
            }
        }
    }
}
