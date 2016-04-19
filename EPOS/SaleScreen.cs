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
    public partial class SaleScreen : Form
    {
        DataTable datatable = new DataTable();
        public SaleScreen()
        {
            InitializeComponent();
        }

        private void SaleScreen_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            Filldatatable(Globals.MenuNo);
            popbuttons();
            populatedatagrid();
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            labelName.Text = Globals.Pubname;
            labelStaffName.Text = "Server: " + Globals.Username;
            dataGridView1.BackgroundColor= Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.ForeColor= Color.FromName(Globals.Fontcolor);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }
        private void Filldatatable(int menuid)
        {
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }
        private void popbuttons()
        {

            int i = 1;
            while (i != 57)
            {
                string butname = "button";
                butname += i.ToString();
                Button control = (Button)this.Controls.Find(butname, true).FirstOrDefault();
                if (datatable.Rows[i - 1][6].ToString() != "Y")
                {
                    control.Visible = false;
                }
                else
                {
                    control.BackColor = Color.FromName(datatable.Rows[i - 1][2].ToString());
                    control.ForeColor = Color.FromName(datatable.Rows[i - 1][5].ToString());
                    control.Font = new Font(datatable.Rows[i - 1][3].ToString(), (int)datatable.Rows[i - 1][4]);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void populatedatagrid()
        {
            decimal total = 0m;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT BasketID, Product.Name, Unit.Name, Price FROM Basket, SaleUnit, Product, Unit WHERE Basket.SaleUnitID = SaleUnit.SaleUnitID AND SaleUnit.ProductID = Product.ProductID AND SaleUnit.UnitID = Unit.UnitID AND StaffID = @staffid", basket);
            basket.Open();
            show.Parameters.AddWithValue("@staffid", Globals.Userno);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                string[] row1 = new string[] { reader.GetInt32(0).ToString(), reader.GetString(1) + ": " + reader.GetString(2), "£" + reader.GetDecimal(3).ToString() };
                dataGridView1.Rows.Add(row1);
                total += reader.GetDecimal(3);
            }
            basket.Close();
            labelTotal.Text = "£" + total.ToString();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ClearSelection();
        }
        private void buttonfunction(int btnno)
        {
            switch (datatable.Rows[btnno - 1][7].ToString())
            {
                case "Product":
                    SqlConnection bsk = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Basket (StaffID, SaleUnitID) VALUES (@staff, @saleunit)", bsk);
                    bsk.Open();
                    show.Parameters.AddWithValue("@staff", Globals.Userno);
                    show.Parameters.AddWithValue("@saleunit", (int)datatable.Rows[btnno - 1][8]);
                    show.ExecuteNonQuery();
                    bsk.Close();
                    populatedatagrid();
                    break;
                case "Menu":
                    Globals.MenuNo = (int)datatable.Rows[btnno - 1][9];
                    Filldatatable(Globals.MenuNo);
                    popbuttons();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonfunction(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonfunction(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonfunction(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonfunction(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            buttonfunction(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            buttonfunction(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            buttonfunction(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            buttonfunction(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            buttonfunction(9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            buttonfunction(10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            buttonfunction(11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            buttonfunction(12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            buttonfunction(13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            buttonfunction(14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            buttonfunction(15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            buttonfunction(16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            buttonfunction(17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            buttonfunction(18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            buttonfunction(19);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            buttonfunction(20);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            buttonfunction(21);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            buttonfunction(22);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            buttonfunction(23);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            buttonfunction(24);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            buttonfunction(25);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            buttonfunction(26);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            buttonfunction(27);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            buttonfunction(28);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            buttonfunction(29);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            buttonfunction(30);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            buttonfunction(31);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            buttonfunction(32);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            buttonfunction(33);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            buttonfunction(34);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            buttonfunction(35);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            buttonfunction(36);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            buttonfunction(37);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            buttonfunction(38);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            buttonfunction(39);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            buttonfunction(40);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            buttonfunction(41);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            buttonfunction(42);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            buttonfunction(43);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            buttonfunction(44);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            buttonfunction(45);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            buttonfunction(46);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            buttonfunction(47);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            buttonfunction(48);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            buttonfunction(49);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            buttonfunction(50);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            buttonfunction(51);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            buttonfunction(52);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            buttonfunction(53);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            buttonfunction(54);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            buttonfunction(55);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            buttonfunction(56);
        }

        private void buttonVoid_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells[0].Value);
                SqlConnection del = new SqlConnection(Globals.dataconnection);
                del.Open();
                SqlCommand delete = new SqlCommand("DELETE FROM Basket WHERE BasketID = @id", del);
                delete.Parameters.AddWithValue("@id", a);
                delete.ExecuteNonQuery();
                del.Close();
                SqlConnection voi = new SqlConnection(Globals.dataconnection);
                SqlCommand add = new SqlCommand("INSERT INTO Transactions (Type, Time, StaffID) VALUES (@type, @time, @staffid)", voi);
                voi.Open();
                add.Parameters.AddWithValue("@type", "Void");
                add.Parameters.AddWithValue("@time", DateTime.Now);
                add.Parameters.AddWithValue("@staffid", Globals.Userno);
                add.ExecuteNonQuery();
                voi.Close();
                populatedatagrid();
            }
            else
            {
                MessageBox.Show("Please select an item to void");
            }
        }
    }
}
