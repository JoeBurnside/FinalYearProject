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
    public partial class AddEditDiscount : Form
    {
        public AddEditDiscount()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0)
            {
                labelSymbol.Text = "P";
            }
            else
            {
                labelSymbol.Text = "%";
            }
        }

        private void AddEditDiscount_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            Fillbox();
            textBoxName.Click += new EventHandler(textBoxName_Click);
            textBoxAmount.Click += new EventHandler(textBoxAmount_Click);
        }
        private void textBoxName_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxName.Text;
            Keyboard keyboard = new Keyboard();
            keyboard.ShowDialog();
            this.ActiveControl = null;
            textBoxName.Text = Globals.KeyboardString;
        }
        private void textBoxAmount_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxAmount.Text;
            Numberpad numpad = new Numberpad();
            numpad.ShowDialog();
            this.ActiveControl = null;
            textBoxAmount.Text = Globals.KeyboardString;
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
                SqlConnection dis = new SqlConnection(Globals.dataconnection);
                SqlCommand sh = new SqlCommand("SELECT * FROM Discount WHERE DiscountID = @id", dis);
                sh.Parameters.AddWithValue("@id", Globals.IDNo);
                dis.Open();
                SqlDataReader reader = sh.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(1);
                    String type = reader.GetString(2);
                    if (type == "Pence")
                    {
                        comboBoxType.SelectedIndex = 0;
                        textBoxAmount.Text = reader.GetInt32(3).ToString();
                    }
                    else
                    {
                        comboBoxType.SelectedIndex = 1;
                        textBoxAmount.Text = reader.GetInt32(4).ToString();
                    }
                    
                }
                dis.Close();
            }
            else
            {
                comboBoxType.SelectedIndex = 0;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (textBoxName.Text.Length <= 25)
                {
                    int id;
                    if (int.TryParse(textBoxAmount.Text, out id))
                    {
                        if (id < 100)
                        {
                            if (id < 0)
                            {
                                MessageBox.Show("Discount amount must be a positive number");
                            }
                            else
                            {
                                addeditdiscount(id);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Discount amount is too large");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Discount amount must be a whole number");
                    }
                }
                else
                {
                    MessageBox.Show("Discount name is too long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a user name");
            }
        }
        private void addeditdiscount(int id)
        {
            if (Globals.IDNo != -1)
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    SqlConnection discount = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE Discount SET Name=@name, Type=@type, Pence=@amount WHERE DiscountID = @id", discount);
                    discount.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.Parameters.AddWithValue("@type", "Pence");
                    show.Parameters.AddWithValue("@amount", id);
                    show.Parameters.AddWithValue("@id", Globals.IDNo);
                    show.ExecuteNonQuery();
                    discount.Close();
                }
                else
                {
                    SqlConnection discount = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE Discount SET Name=@name, Type=@type, Percen=@amount WHERE DiscountID = @id", discount);
                    discount.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.Parameters.AddWithValue("@type", "Percen");
                    show.Parameters.AddWithValue("@amount", id);
                    show.Parameters.AddWithValue("@id", Globals.IDNo);
                    show.ExecuteNonQuery();
                    discount.Close();
                }
                
            }
            else
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    SqlConnection discount = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Discount (Name, Type, Pence) VALUES (@name, @type, @amount)", discount);
                    discount.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.Parameters.AddWithValue("@type", "Pence");
                    show.Parameters.AddWithValue("@amount", id);
                    show.ExecuteNonQuery();
                    discount.Close();
                }
                else
                {
                    SqlConnection discount = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Discount (Name, Type, Percen) VALUES (@name, @type, @amount)", discount);
                    discount.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.Parameters.AddWithValue("@type", "Percen");
                    show.Parameters.AddWithValue("@amount", id);
                    show.ExecuteNonQuery();
                    discount.Close();
                }
            }
            this.Close();
        }
    }
}
