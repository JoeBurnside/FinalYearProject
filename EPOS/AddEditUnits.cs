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
    public partial class AddEditUnits : Form
    {
        public AddEditUnits()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditUnits_Load(object sender, EventArgs e)
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
                SqlConnection unit = new SqlConnection(Globals.dataconnection);
                SqlCommand show = new SqlCommand("SELECT Name FROM Unit WHERE UnitID = @id", unit);
                show.Parameters.AddWithValue("@id", Globals.IDNo);
                unit.Open();
                SqlDataReader reader = show.ExecuteReader();
                while (reader.Read())
                {
                    textBoxName.Text = reader.GetString(0);
                }
                unit.Close();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (Globals.IDNo != -1)
                {
                    SqlConnection uni = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("UPDATE Unit SET Name = @name WHERE UnitID = @id", uni);
                    uni.Open();
                    show.Parameters.AddWithValue("@id", Globals.IDNo);
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    uni.Close();
                }
                else
                {
                    SqlConnection uni = new SqlConnection(Globals.dataconnection);
                    SqlCommand show = new SqlCommand("INSERT INTO Unit (Name) VALUES (@name)", uni);
                    uni.Open();
                    show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.ExecuteNonQuery();
                    uni.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a unit name");
            }
        }
    }
}
