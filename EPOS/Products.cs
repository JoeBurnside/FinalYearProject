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
    public partial class Products : Form
    {
        List<ListObject> theList = new List<ListObject>();
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();

            fillbox();
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            listBox1.BackColor = Color.FromName(Globals.Backcolor);
            listBox1.ForeColor = Color.FromName(Globals.Fontcolor);
            labelName.Text = Globals.Pubname;
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fillbox()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            theList.Clear();
            SqlConnection product = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT ProductID, Name FROM Product ORDER BY Name", product);
            product.Open();
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList.Add(new ListObject(reader.GetString(1), reader.GetInt32(0)));
            }
            product.Close();
            listBox1.DataSource = theList;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
            listBox1.SelectedIndex = -1;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this product?",
                      "Delete?", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    int id = int.Parse(listBox1.SelectedValue.ToString());
                    SqlConnection prod = new SqlConnection(Globals.dataconnection);
                    prod.Open();
                    SqlCommand delete = new SqlCommand("DELETE FROM Product WHERE ProductID = @id", prod);
                    delete.Parameters.AddWithValue("@id", id);
                    delete.ExecuteNonQuery();
                    prod.Close();
                    break;
                case DialogResult.No: break;
            }
            }
            else {
                MessageBox.Show("Please select a product from the list");
            }
            fillbox();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {

            }
            else {
                MessageBox.Show("Please select a product from the list");
            }
        }
    }
}
