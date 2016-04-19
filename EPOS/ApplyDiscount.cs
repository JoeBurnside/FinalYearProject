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
    public partial class ApplyDiscount : Form
    {
        List<ListObject> theList = new List<ListObject>();
        List<ListObject> theList2 = new List<ListObject>();
        List<ListObject> theList3 = new List<ListObject>();
        public ApplyDiscount()
        {
            InitializeComponent();
        }

        private void ApplyDiscount_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            filllist();
            filllist2();
            fillbox();
            comboBoxUnit.SelectedValueChanged += ComboBoxUnit_SelectedValueChanged;
        }

        private void ComboBoxUnit_SelectedValueChanged(object sender, EventArgs e)
        {
            fillbox();
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
            if (comboBoxUnit.SelectedValue != null)
            {
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                theList.Clear();
                SqlConnection discount = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, Discount.Name, Discount.DiscountID FROM Product, Unit, SaleUnit, ApplyDiscount, Discount  WHERE Unit.UnitID = SaleUnit.UnitID AND Product.ProductID = SaleUnit.ProductID AND SaleUnit.SaleUnitID = ApplyDiscount.SaleUnitID AND ApplyDiscount.DiscountID = Discount.DiscountID AND SaleUnit.SaleUnitID = @id", discount);
                discount.Open();
                show.Parameters.AddWithValue("@id", int.Parse(comboBoxUnit.SelectedValue.ToString()));
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    theList.Add(new ListObject(reader.GetString(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2), reader.GetInt32(3)));
                }
                discount.Close();
                listBox1.DataSource = theList;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "ID";
                listBox1.SelectedIndex = -1;
            }
        }
        private void filllist()
        {
            comboBoxUnit.DataSource = null;
            comboBoxUnit.Items.Clear();
            theList2.Clear();
            SqlConnection unit = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT SaleUnit.SaleUnitID, Unit.Name FROM Unit, SaleUnit WHERE Unit.UnitID = SaleUnit.UnitID AND SaleUnit.ProductID = @id  ORDER BY Name", unit);
            unit.Open();
            show.Parameters.AddWithValue("@id", Globals.IDNo);
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
        private void filllist2()
        {
            comboBoxDiscount.DataSource = null;
            comboBoxDiscount.Items.Clear();
            theList3.Clear();
            SqlConnection dis = new SqlConnection(Globals.dataconnection);
            SqlCommand showdis = new SqlCommand("SELECT DiscountID, Name FROM Discount ORDER BY Name", dis);
            dis.Open();
            SqlDataReader reader2 = showdis.ExecuteReader();
            while (reader2.Read())
            {
                theList3.Add(new ListObject(reader2.GetString(1), reader2.GetInt32(0)));
            }
            dis.Close();
            comboBoxDiscount.DataSource = theList3;
            comboBoxDiscount.DisplayMember = "Name";
            comboBoxDiscount.ValueMember = "ID";
            comboBoxDiscount.SelectedIndex = 0;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection addcheck = new SqlConnection(Globals.dataconnection);
            SqlCommand showcheck = new SqlCommand("SELECT * FROM ApplyDiscount WHERE DiscountID=@disid AND SaleUnitID=@unitid", addcheck);
            addcheck.Open();
            showcheck.Parameters.AddWithValue("@disid", int.Parse(comboBoxDiscount.SelectedValue.ToString()));
            showcheck.Parameters.AddWithValue("@unitid", int.Parse(comboBoxUnit.SelectedValue.ToString()));
            SqlDataReader readerx = showcheck.ExecuteReader();
            if (readerx.HasRows)
            {
                MessageBox.Show("Discount already applied");
                addcheck.Close();
            }
            else
            {
                addcheck.Close();
                SqlConnection adddis = new SqlConnection(Globals.dataconnection);
                SqlCommand addd = new SqlCommand("INSERT INTO ApplyDiscount (SaleUnitID, DiscountID) VALUES (@unitid, @disid)", adddis);
                adddis.Open();
                addd.Parameters.AddWithValue("@unitid", int.Parse(comboBoxUnit.SelectedValue.ToString()));
                addd.Parameters.AddWithValue("@disid", int.Parse(comboBoxDiscount.SelectedValue.ToString()));
                addd.ExecuteNonQuery();
                adddis.Close();
                fillbox();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this discount?",
                      "Delete?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        int id = int.Parse(listBox1.SelectedValue.ToString());
                        SqlConnection prod = new SqlConnection(Globals.dataconnection);
                        prod.Open();
                        SqlCommand delete = new SqlCommand("DELETE FROM ApplyDiscount WHERE SaleUnitID = @unitid AND DiscountID = @id", prod);
                        delete.Parameters.AddWithValue("@unitid", int.Parse(comboBoxUnit.SelectedValue.ToString()));
                        delete.Parameters.AddWithValue("@id", id);
                        delete.ExecuteNonQuery();
                        prod.Close();
                        fillbox();
                        break;
                    case DialogResult.No: break;
                }
            }
            else {
                MessageBox.Show("Please select a discount from the list");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
