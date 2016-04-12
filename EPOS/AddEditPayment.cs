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
    public partial class AddEditPayment : Form
    {
        public AddEditPayment()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (Globals.IDNo != -1)
                {
                    SqlConnection pay = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE Payment SET Name = @name WHERE PaymentID = @id", pay);
                    pay.Open();
                    show.Parameters.AddWithValue("@id", Globals.IDNo);
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    pay.Close();
                }
                else
                {
                    SqlConnection pay = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Payment (Name) VALUES (@name)", pay);
                    pay.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    pay.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a payment method name");
            }
        }

        private void AddEditPayment_Load(object sender, EventArgs e)
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
            if (Globals.IDNo != -1)
            {
                SqlConnection payment = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Name FROM Payment WHERE PaymentID = @id", payment);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                payment.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(0);
                }
                payment.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
