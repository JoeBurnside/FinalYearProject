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
    public partial class AddMenu : Form
    {
        public AddMenu()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddMenu_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (textBoxName.Text.Length <= 50)
                {
                    SqlConnection menu = new SqlConnection(Globals.dataconnection);
                    SqlCommand add = new SqlCommand("INSERT INTO Menu (Name) VALUES (@name); SELECT SCOPE_IDENTITY();", menu);
                    menu.Open();
                    add.Parameters.AddWithValue("@name", textBoxName.Text);
                    SqlDataReader reader = add.ExecuteReader();
                    while (reader.Read())
                    {
                        Globals.MenuNo = (int)reader.GetDecimal(0);
                    }
                    menu.Close();
                    int i = 1;
                    while (i != 57)
                    {
                        SqlConnection button = new SqlConnection(Globals.dataconnection);
                        SqlCommand insert = new SqlCommand("INSERT INTO Button (MenuID, ButtonID, Color, Font, FontSize, FontColor, Active, Type) VALUES (@menuid, @buttonid, @color, @font, @fontsize, @fontcolor, @active, @type)", button);
                        button.Open();
                        insert.Parameters.AddWithValue("@menuid", Globals.MenuNo);
                        insert.Parameters.AddWithValue("@buttonid", i);
                        insert.Parameters.AddWithValue("@color", "Silver");
                        insert.Parameters.AddWithValue("@font", "Microsoft Sans Serif");
                        insert.Parameters.AddWithValue("@fontsize", 10);
                        insert.Parameters.AddWithValue("@fontcolor", "Black");
                        insert.Parameters.AddWithValue("@active", "N");
                        insert.Parameters.AddWithValue("@type", "Blank");
                        insert.ExecuteNonQuery();
                        button.Close();
                        i++;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Menu name is too long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a menu name");
            }
        }
    }
}
