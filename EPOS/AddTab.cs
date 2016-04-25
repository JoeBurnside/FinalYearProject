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
    public partial class AddTab : Form
    {
        public AddTab()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTab_Load(object sender, EventArgs e)
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
                if (textBoxName.Text.Length <= 25)
                {
                        SqlConnection cat = new SqlConnection(Globals.dataconnection);
                        SqlCommand show = new SqlCommand("INSERT INTO Tab (Name, Opened, OpenedBy, LastAccessed, AccessedBy) VALUES (@name, @opened, @openedby, @accessed, @accessedby)", cat);
                        cat.Open();
                        show.Parameters.AddWithValue("@name", textBoxName.Text);
                    show.Parameters.AddWithValue("@opened", DateTime.Now);
                    show.Parameters.AddWithValue("@openedby", Globals.Userno);
                    show.Parameters.AddWithValue("@accessed", DateTime.Now);
                    show.Parameters.AddWithValue("@accessedby", Globals.Userno);
                    show.ExecuteNonQuery();
                        cat.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tab name is too long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a tab name");
            }
        }
    }
}