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
    public partial class Pay : Form
    {
        DataTable datatablepay = new DataTable();
        DataTable datatabledis = new DataTable();
        DataTable datatabletrans = new DataTable();
        decimal discount;
        public Pay()
        {
            InitializeComponent();
        }
        private void filldatatable()
        {
            datatablepay.Clear();
            SqlConnection table = new SqlConnection(Globals.dataconnection);
            SqlCommand fill = new SqlCommand("SELECT * FROM Payment ORDER BY PaymentID", table);
            table.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = fill;
            da.Fill(datatablepay);
            table.Close();
            datatabledis.Clear();
            SqlConnection table2 = new SqlConnection(Globals.dataconnection);
            SqlCommand fill2 = new SqlCommand("SELECT * FROM Discount ORDER BY DiscountID", table2);
            table2.Open();
            SqlDataAdapter da2 = new SqlDataAdapter();
            da2.SelectCommand = fill2;
            da2.Fill(datatabledis);
            table.Close();
        }

        private void buttonVoid_Click(object sender, EventArgs e)
        {
            if (Globals.UserLevel == "Staff")
            {
                MessageBox.Show("Insufficient Authority");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction(1, Globals.Total + Globals.Total, "Refund");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ".";
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 1)
            { textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1); }
        }

        private void Pay_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            populatedatagrid();
            filldatatable();
            popbuttons();
            discount = 0m;
            Globals.Cash = 0m;
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            labelName.Text = Globals.Pubname;
            labelStaffName.Text = "Server: " + Globals.Username;
            dataGridView1.BackgroundColor = Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void populatedatagrid()
        {
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
            }
            basket.Close();
            labelTotal.Text = "£" + Globals.Total.ToString();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ClearSelection();
        }
        private void popbuttons()
        {

            int i = 1;
            while (i != datatablepay.Rows.Count)
            {
                string butname = "buttonPay";
                butname += i.ToString();
                Button control = (Button)this.Controls.Find(butname, true).FirstOrDefault();
                control.Visible = true;
                control.Text = datatablepay.Rows[i][1].ToString();
                i++;
            }
            int i2 = 1;
            while (i2 != datatabledis.Rows.Count + 1)
            {
                string butname = "buttonDiscount";
                butname += i2.ToString();
                Button control = (Button)this.Controls.Find(butname, true).FirstOrDefault();
                control.Visible = true;
                control.Text = datatabledis.Rows[i2 - 1][1].ToString();
                i2++;
            }
        }

        private void buttonPay7_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[7][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonDiscount2_Click(object sender, EventArgs e)
        {
            dodiscount(1);
        }

        private void buttonDiscount3_Click(object sender, EventArgs e)
        {
            dodiscount(2);
        }
        private void maketransaction(int paymenttype, decimal amount, string transtype)
        {
            Globals.Cash += amount;
            Globals.Change = Globals.Cash - Globals.Total;
            if (Globals.Change < 0)
            {
                buttonBalance.Visible = true;
                buttonBalance.ForeColor = Color.FromName("Blue");
                buttonBalance.BackColor = Color.FromName("Red");
                buttonBalance.Text = "Remaining Balance:" + Environment.NewLine + "£" + Math.Abs(Globals.Change).ToString();
            }
            else
            {
                Printer.OpenTill();
                SqlConnection createtrans = new SqlConnection(Globals.dataconnection);
                SqlCommand make = new SqlCommand("INSERT INTO Transactions (Type, Time, StaffID, PaymentID, Discount) VALUES (@type, @time, @staff, @payment, @discount); SELECT SCOPE_IDENTITY();", createtrans);
                createtrans.Open();
                make.Parameters.AddWithValue("@type", transtype);
                make.Parameters.AddWithValue("@time", DateTime.Now);
                make.Parameters.AddWithValue("@staff", Globals.Userno);
                make.Parameters.AddWithValue("@payment", paymenttype);
                make.Parameters.AddWithValue("@discount", discount);
                SqlDataReader reader = make.ExecuteReader();
                while (reader.Read())
                {
                    Globals.IDNo = (int)reader.GetDecimal(0);
                }
                createtrans.Close();
                SqlConnection basket = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT SaleUnitID FROM Basket WHERE StaffID = @staffid;", basket);
                basket.Open();
                show.Parameters.AddWithValue("@staffid", Globals.Userno);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = show;
                da.Fill(datatabletrans);
                basket.Close();
                int index = 0;
                while (index != datatabletrans.Rows.Count)
                {
                    SqlConnection newtrans = new SqlConnection(Globals.dataconnection);
                    SqlCommand add = new SqlCommand("INSERT INTO TransactionItem (TransactionID, SaleUnitID) VALUES (@trans, @saleunit)", newtrans);
                    newtrans.Open();
                    add.Parameters.AddWithValue("@trans", Globals.IDNo);
                    add.Parameters.AddWithValue("@saleunit", (int)datatabletrans.Rows[index][0]);
                    add.ExecuteNonQuery();
                    newtrans.Close();
                    index++;
                }
                SqlConnection cat = new SqlConnection(Globals.dataconnection);
                cat.Open();
                SqlCommand delete = new SqlCommand("DELETE FROM Basket WHERE StaffID = @id", cat);
                delete.Parameters.AddWithValue("@id", Globals.Userno);
                delete.ExecuteNonQuery();
                cat.Close();
                SaleDone saledone = new SaleDone();
                saledone.ShowDialog();
                this.Close();
            }
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            if (Globals.Total > 0m)
            {
                maketransaction(1, 5m, "Sale");
            }
        }

        private void buttonTen_Click(object sender, EventArgs e)
        {
            if (Globals.Total > 0m)
            {
                maketransaction(1, 10m, "Sale");
            }
        }

        private void buttonTwenty_Click(object sender, EventArgs e)
        {
            if (Globals.Total > 0m)
            {
                maketransaction(1, 20m, "Sale");
            }
        }

        private void buttonFifty_Click(object sender, EventArgs e)
        {
            if (Globals.Total > 0m)
            {
                maketransaction(1, 50m, "Sale");
            }
        }

        private void buttonCash_Click(object sender, EventArgs e)
        {
            if (Globals.Total > 0m)
            {
                if (textBox1.Text != "")
                {
                    try
                    {
                        decimal cash = Decimal.Parse(textBox1.Text);
                        Math.Round(cash, 2);
                        maketransaction(1, cash, "Sale");
                    }
                    catch
                    {
                        MessageBox.Show("Cash value entered is incorrect");
                    }
                    
                }
                else
                {
                    maketransaction(1, Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay1_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[1][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay2_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[2][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay3_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[3][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay4_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[4][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay5_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[5][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay6_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[6][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay8_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[8][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay9_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[9][0], Globals.Total, "Sale");
                }
            }
        }

        private void buttonPay10_Click(object sender, EventArgs e)
        {
            if (Globals.Cash > 0m)
            {
                MessageBox.Show("Please continue with cash payment");
            }
            else
            {
                if (Globals.Total > 0m)
                {
                    maketransaction((int)datatablepay.Rows[10][0], Globals.Total, "Sale");
                }
            }
        }
        private void dodiscount(int rowno)
        {
            if (discount != 0m)
            {
                MessageBox.Show("Discount already applied");
            }
            else {
                decimal disamount;
                switch (datatabledis.Rows[rowno][2].ToString())
                {
                    case "Pence":
                        disamount = decimal.Parse(datatabledis.Rows[rowno][3].ToString());
                        SqlConnection disc = new SqlConnection(Globals.dataconnection);
                        SqlCommand find = new SqlCommand("SELECT SaleUnit.Price FROM Basket, SaleUnit, ApplyDiscount WHERE Basket.SaleUnitID = SaleUnit.SaleUnitID AND SaleUnit.SaleUnitID = ApplyDiscount.SaleUnitID AND DiscountID = @discount AND StaffID = @staffid", disc);
                        disc.Open();
                        find.Parameters.AddWithValue("@discount", (int)datatabledis.Rows[rowno][0]);
                        find.Parameters.AddWithValue("@staffid", Globals.Userno);
                        SqlDataReader readerdis = find.ExecuteReader();
                        while (readerdis.Read())
                        {
                            discount += disamount / 100;
                        }
                        disc.Close();
                        break;
                    case "Percen":
                        disamount = decimal.Parse(datatabledis.Rows[rowno][4].ToString());
                        SqlConnection disc1 = new SqlConnection(Globals.dataconnection);
                        SqlCommand find1 = new SqlCommand("SELECT SaleUnit.Price FROM Basket, SaleUnit, ApplyDiscount WHERE Basket.SaleUnitID = SaleUnit.SaleUnitID AND SaleUnit.SaleUnitID = ApplyDiscount.SaleUnitID AND DiscountID = @discount AND StaffID = @staffid", disc1);
                        disc1.Open();
                        find1.Parameters.AddWithValue("@discount", (int)datatabledis.Rows[rowno][0]);
                        find1.Parameters.AddWithValue("@staffid", Globals.Userno);
                        SqlDataReader readerdis1 = find1.ExecuteReader();
                        while (readerdis1.Read())
                        {
                            discount += readerdis1.GetDecimal(0) * disamount / 100;
                        }
                        disc1.Close();
                        break;
                }
               
                if (discount > 0m)
                {
                    Math.Round(discount, 2);
                    Globals.Total -= discount;
                    labelTotal.Text = "£" + Globals.Total;
                    string[] row2 = new string[] { "0", datatabledis.Rows[rowno][1].ToString(), "-£" + string.Format("{0:0.00}", discount) };
                    dataGridView1.Rows.Add(row2);
                }
            }

        }

        private void buttonDiscount1_Click(object sender, EventArgs e)
        {
            dodiscount(0);
        }

        private void buttonDiscount4_Click(object sender, EventArgs e)
        {
            dodiscount(3);
        }

        private void buttonDiscount5_Click(object sender, EventArgs e)
        {
            dodiscount(4);
        }

        private void buttonDiscount6_Click(object sender, EventArgs e)
        {
            dodiscount(5);
        }

        private void buttonDiscount7_Click(object sender, EventArgs e)
        {
            dodiscount(6);
        }

        private void buttonDiscount8_Click(object sender, EventArgs e)
        {
            dodiscount(7);
        }

        private void buttonDiscount9_Click(object sender, EventArgs e)
        {
            dodiscount(8);
        }

        private void buttonDiscount10_Click(object sender, EventArgs e)
        {
            dodiscount(9);
        }

        private void buttonDiscount11_Click(object sender, EventArgs e)
        {
            dodiscount(10);
        }

        private void buttonDiscount12_Click(object sender, EventArgs e)
        {
            dodiscount(11);
        }

        private void buttonTabs_Click(object sender, EventArgs e)
        {
            Tabs tabs = new Tabs();
            tabs.ShowDialog();
            this.Close();
        }
    }
}