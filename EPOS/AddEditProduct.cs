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
    public partial class AddEditProduct : Form
    {
        List<ListObject> theList = new List<ListObject>();
        public AddEditProduct()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (textBoxName.Text.Length <= 255)
                {
                    if (Globals.IDNo != -1)
                    {
                        SqlConnection prod = new SqlConnection(Globals.dataconnection);
                        SqlCommand show = new SqlCommand("UPDATE Product SET Name = @name, CategoryID = @catid WHERE ProductID = @id", prod);
                        prod.Open();
                        show.Parameters.AddWithValue("@catid", int.Parse(comboBoxCat.SelectedValue.ToString()));
                        show.Parameters.AddWithValue("@name", textBoxName.Text);
                        show.Parameters.AddWithValue("@id", Globals.IDNo);
                        show.ExecuteNonQuery();
                        prod.Close();
                    }
                    else
                    {
                        SqlConnection cat = new SqlConnection(Globals.dataconnection);
                        SqlCommand show = new SqlCommand("INSERT INTO Product (Name, CategoryID) VALUES (@name, @catid); SELECT SCOPE_IDENTITY();", cat);
                        cat.Open();
                        show.Parameters.AddWithValue("@name", textBoxName.Text);
                        show.Parameters.AddWithValue("@catid", int.Parse(comboBoxCat.SelectedValue.ToString()));
                        SqlDataReader reader = show.ExecuteReader();
                        while (reader.Read())
                        {
                            Globals.IDNo = (int)reader.GetDecimal(0);
                        }
                        cat.Close();
                    }
                    SaleUnit salesunit = new SaleUnit();
                    salesunit.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Product name is too long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a product name");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditProduct_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            Filllist();
            Fillbox();
            textBoxName.Click += new EventHandler(textBoxName_Click);
        }
        private void textBoxName_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxName.Text;
            Keyboard keyboard = new Keyboard();
            keyboard.ShowDialog();
            this.ActiveControl = null;
            textBoxName.Text = Globals.KeyboardString;
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
        private void Fillbox()
        {
            if (Globals.IDNo != -1)
            {
                SqlConnection product = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Name, CategoryID FROM Product WHERE ProductID = @id", product);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                product.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(0);
                    comboBoxCat.SelectedValue = reader.GetInt32(1);
                }
                product.Close();
            }
        }
        private void Filllist()
        {
            comboBoxCat.DataSource = null;
            comboBoxCat.Items.Clear();
            theList.Clear();
            SqlConnection category = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT CategoryID, Name FROM Category ORDER BY Name", category);
            category.Open();
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList.Add(new ListObject(reader.GetString(1), reader.GetInt32(0)));
            }
            category.Close();
            comboBoxCat.DataSource = theList;
            comboBoxCat.DisplayMember = "Name";
            comboBoxCat.ValueMember = "ID";
            comboBoxCat.SelectedIndex = 0;
        }
    }
}
