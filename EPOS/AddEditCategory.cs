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
    public partial class AddEditCategory : Form
    {
        public AddEditCategory()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (Globals.IDNo != -1)
                {
                    SqlConnection cat = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE Category SET Name = @name WHERE CategoryID = @id", cat);
                    cat.Open();
                    show.Parameters.AddWithValue("@id", Globals.IDNo);
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    cat.Close();
                }
                else
                {
                    SqlConnection cat = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Category (Name) VALUES (@name)", cat);
                    cat.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    cat.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a category name");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxButton_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddEditCategory_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
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
            if (Globals.IDNo!=-1)
            {
                SqlConnection category = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Name FROM Category WHERE CategoryID = @id", category);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                category.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(0);
                }
                category.Close();
            }
        }
    }
}
