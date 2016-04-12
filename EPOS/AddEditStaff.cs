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
    public partial class AddEditStaff : Form
    {
        public AddEditStaff()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditStaff_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            Fillbox();
            textBoxName.Click += new EventHandler(textBoxName_Click);
            textBoxId.Click += new EventHandler(textBoxId_Click);
        }
        private void textBoxName_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxName.Text;
            Keyboard keyboard = new Keyboard();
            keyboard.ShowDialog();
            this.ActiveControl = null;
            textBoxName.Text = Globals.KeyboardString;
        }
        private void textBoxId_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBoxId.Text;
            Numberpad numpad = new Numberpad();
            numpad.ShowDialog();
            this.ActiveControl = null;
            textBoxId.Text = Globals.KeyboardString;
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
                SqlConnection staff = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT * FROM Staff WHERE StaffID = @id", staff);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                staff.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(1);
                    textBoxId.Text = reader.GetInt32(0).ToString();
                    comboBoxLevel.SelectedItem = reader.GetString(2);
                }
                staff.Close();
                textBoxId.Enabled= false;
            }
            else
            {
                comboBoxLevel.SelectedItem = "Staff";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (textBoxId.Text.Length == 4)
                {
                    int id;
                    if (int.TryParse(textBoxId.Text, out id))
                    {
                        SqlConnection sta = new SqlConnection(Globals.dataconnection);
                        SqlCommand sho = new SqlCommand("SELECT * FROM Staff WHERE StaffID = @id", sta);
                        sho.Parameters.AddWithValue("@id", id);
                        sta.Open();
                        SqlDataReader reader = sho.ExecuteReader();
                        if (reader.Read())
                        {
                           if (Globals.IDNo == id)
                            {
                                addeditstaff(id);
                            }
                            else
                            {
                                MessageBox.Show("Staff ID Number already in use");
                            }
                        }
                        else
                        {
                            addeditstaff(id);
                        }
                        sta.Close();
                        
                       
                    }
                    else
                    {
                        MessageBox.Show("User ID number must be a number");
                    }
                }
                else
                {
                    MessageBox.Show("User ID number must be 4 digits long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a user name");
            }
        }
        private void addeditstaff(int id)
        {
            if (Globals.IDNo != -1)
            {
                SqlConnection staff = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("UPDATE Staff SET Name=@name, Level=@level WHERE StaffID = @id", staff);
                staff.Open();
                show.Parameters.AddWithValue("@name", textBoxName.Text);
                show.Parameters.AddWithValue("@level", comboBoxLevel.SelectedItem);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                show.ExecuteNonQuery();
                staff.Close();
            }
            else
            {
                SqlConnection staff = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("INSERT INTO Staff (StaffID, Name, Level) VALUES (@id, @name, @level)", staff);
                staff.Open();
                show.Parameters.AddWithValue("@id", id);
                show.Parameters.AddWithValue("@name", textBoxName.Text);
                show.Parameters.AddWithValue("@level", comboBoxLevel.SelectedItem);
                show.ExecuteNonQuery();
                staff.Close();
            }
            this.Close();
        }
    }
}
