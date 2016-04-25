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
    public partial class ViewTransactions : Form
    {
        public ViewTransactions()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewTransactions_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            populatedatagrid();
            dataGridView2.SelectionChanged += DataGridView_SelectionChanged;
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.BackgroundColor = Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            dataGridView2.BackgroundColor = Color.FromName(Globals.Backcolor);
            dataGridView2.DefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView2.DefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromName(Globals.Backcolor);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromName(Globals.Fontcolor);
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }
        private void populatedatagrid()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT Transactions.TransactionID, Transactions.Type, Transactions.Time, Staff.Name FROM Transactions, Staff WHERE Staff.StaffID = Transactions.StaffID ORDER BY Transactions.Time DESC", basket);
            basket.Open();
            show.Parameters.AddWithValue("@staffid", Globals.Userno);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                string[] row1 = new string[] { reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetDateTime(2).ToString(), reader.GetString(3) };
                dataGridView2.Rows.Add(row1);
            }
            basket.Close();
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView2.ClearSelection();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                int a = Int32.Parse(selectedRow.Cells[0].Value.ToString());
                Printer.Receipt(a);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                Globals.Total = 0m;
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells[0].Value);
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                SqlConnection transitems = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, Price FROM TransactionItem, SaleUnit, Product, Unit WHERE TransactionItem.SaleUnitID = SaleUnit.SaleUnitID AND SaleUnit.ProductID = Product.ProductID AND SaleUnit.UnitID = Unit.UnitID AND TransactionID = @transid", transitems);
                transitems.Open();
                show.Parameters.AddWithValue("@transid", a);
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    string[] row1 = new string[] { reader.GetString(0)+ ": " + reader.GetString(1), "£" + reader.GetDecimal(2).ToString() };
                    dataGridView1.Rows.Add(row1);
                    Globals.Total += reader.GetDecimal(2);
                }
                transitems.Close();
                SqlConnection dis = new SqlConnection(Globals.dataconnection);
                SqlCommand show2 = new SqlCommand("SELECT Discount FROM Transactions WHERE TransactionID = @transid", dis);
                dis.Open();
                show2.Parameters.AddWithValue("@transid", a);
                SqlDataReader reader2 = show2.ExecuteReader();
                while (reader2.Read())
                {
                    string[] row2 = new string[] { "Discount:", "-£" + reader2.GetDecimal(0).ToString() };
                    Globals.Total -= reader2.GetDecimal(0);
                    dataGridView1.Rows.Add(row2);
                }
                dis.Close();
                labelTotal.Text = "£" + Globals.Total.ToString();
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.ClearSelection();
            }
            }

        }
}
