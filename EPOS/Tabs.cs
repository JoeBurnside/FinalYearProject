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
    public partial class Tabs : Form
    {
        DataTable datatabletrans = new DataTable();
        public Tabs()
        {
            InitializeComponent();
        }

        private void Tabs_Load(object sender, EventArgs e)
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
            SqlCommand show = new SqlCommand("SELECT Tab.TabID, Tab.Name, Tab.Opened, s.Name, Tab.LastAccessed, t.Name FROM Staff s, Staff t, Tab WHERE s.StaffID = Tab.OpenedBy AND t.StaffID = Tab.AccessedBy ORDER BY Tab.TabID", basket);
            basket.Open();
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                string[] row1 = new string[] { reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetDateTime(2).ToString(), reader.GetString(3), reader.GetDateTime(4).ToString(), reader.GetString(5) };
                dataGridView2.Rows.Add(row1);
            }
            basket.Close();
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView2.ClearSelection();
        }
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            fillotherbox();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                int a = Int32.Parse(selectedRow.Cells[0].Value.ToString());
                Printer.Bill(a);
            }
            else
            {
                MessageBox.Show("Please select a tab from the list");
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            AddTab addtab = new AddTab();
            addtab.ShowDialog();
            populatedatagrid();
            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Selected = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                datatabletrans.Rows.Clear();
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                int a = Int32.Parse(selectedRow.Cells[0].Value.ToString());
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
                    SqlCommand add = new SqlCommand("INSERT INTO TabItem (TabID, SaleUnitID) VALUES (@tab, @saleunit)", newtrans);
                    newtrans.Open();
                    add.Parameters.AddWithValue("@tab", a);
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
                SqlConnection upd = new SqlConnection(Globals.dataconnection);
                SqlCommand show2 = new SqlCommand("UPDATE Tab SET LastAccessed = @accessed, AccessedBy = @accessedby WHERE TabID = @id", upd);
                upd.Open();
                show2.Parameters.AddWithValue("@id", a);
                show2.Parameters.AddWithValue("@accessed", DateTime.Now);
                show2.Parameters.AddWithValue("@accessedby", Globals.Userno);
                show2.ExecuteNonQuery();
                upd.Close();
                fillotherbox();
            }
            else
            {
                MessageBox.Show("Please select a tab from the list");
            }
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
                    if (dataGridView2.SelectedCells.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to pay this tab now?" + Environment.NewLine + "The tab must be paid in the full amount and cannot be reversed.",
                      "Delete?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        datatabletrans.Rows.Clear();
                        int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                        int a = Int32.Parse(selectedRow.Cells[0].Value.ToString());
                        SqlConnection basket = new SqlConnection(Globals.dataconnection);
                        SqlCommand show = new SqlCommand("SELECT SaleUnitID FROM TabItem WHERE TabID = @id;", basket);
                        basket.Open();
                        show.Parameters.AddWithValue("@id", a);
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = show;
                        da.Fill(datatabletrans);
                        basket.Close();
                        int index = 0;
                        while (index != datatabletrans.Rows.Count)
                        {
                            SqlConnection newtrans = new SqlConnection(Globals.dataconnection);
                            SqlCommand add = new SqlCommand("INSERT INTO Basket (StaffID, SaleUnitID) VALUES (@staff, @saleunit)", newtrans);
                            newtrans.Open();
                            add.Parameters.AddWithValue("@staff", Globals.Userno);
                            add.Parameters.AddWithValue("@saleunit", (int)datatabletrans.Rows[index][0]);
                            add.ExecuteNonQuery();
                            newtrans.Close();
                            index++;
                        }
                        SqlConnection cat = new SqlConnection(Globals.dataconnection);
                        cat.Open();
                        SqlCommand delete = new SqlCommand("DELETE FROM Tab WHERE TabID = @id", cat);
                        delete.Parameters.AddWithValue("@id", a);
                        delete.ExecuteNonQuery();
                        cat.Close();
                        Pay pay = new Pay();
                        pay.ShowDialog();
                        this.Close();
                        break;
                    case DialogResult.No:
                        break;
                }
                }
            else
            {
                MessageBox.Show("Please select a tab from the list");
            }

        }
        private void fillotherbox()
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
                SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, Price FROM TabItem, SaleUnit, Product, Unit WHERE TabItem.SaleUnitID = SaleUnit.SaleUnitID AND SaleUnit.ProductID = Product.ProductID AND SaleUnit.UnitID = Unit.UnitID AND TabID = @transid", transitems);
                transitems.Open();
                show.Parameters.AddWithValue("@transid", a);
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    string[] row1 = new string[] { reader.GetString(0) + ": " + reader.GetString(1), "£" + reader.GetDecimal(2).ToString() };
                    dataGridView1.Rows.Add(row1);
                    Globals.Total += reader.GetDecimal(2);
                }
                transitems.Close();
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
