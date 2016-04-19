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
    public partial class Staff : Form
    {
        List<ListObject> theList = new List<ListObject>();
        public Staff()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void labelDate_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this user?",
                      "Delete?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        try {
                            int id = int.Parse(listBox1.SelectedValue.ToString());
                            SqlConnection sta = new SqlConnection(Globals.dataconnection);
                            sta.Open();
                            SqlCommand delete = new SqlCommand("DELETE FROM Staff WHERE StaffID = @id", sta);
                            delete.Parameters.AddWithValue("@id", id);
                            delete.ExecuteNonQuery();
                            sta.Close();
                        }
                        catch
                        {
                            MessageBox.Show("This user can not be deleted as their account is currently in use");
                        }
                        break;
                    case DialogResult.No: break;
                }

            }
            else {
                MessageBox.Show("Please select a user from the list");
            }
            fillbox();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Globals.IDNo = int.Parse(listBox1.SelectedValue.ToString());
                AddEditStaff addedit = new AddEditStaff();
                addedit.ShowDialog();
                fillbox();
            }
            else {
                MessageBox.Show("Please select a user from the list");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Globals.IDNo = -1;
            AddEditStaff addedit = new AddEditStaff();
            addedit.ShowDialog();
            fillbox();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelName_Click(object sender, EventArgs e)
        {

        }

        private void Staff_Load(object sender, EventArgs e)
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
            labelName.Text = Globals.Pubname;
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }
        private void fillbox()
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            theList.Clear();
            SqlConnection staff = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT StaffID, Name FROM Staff ORDER BY Name", staff);
            staff.Open();
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                theList.Add(new ListObject(reader.GetString(1), reader.GetInt32(0)));
            }
            staff.Close();
            listBox1.DataSource = theList;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
            listBox1.SelectedIndex = -1;
        }
    }
}
