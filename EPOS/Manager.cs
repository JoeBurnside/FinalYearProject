using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonNoSale_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void buttonShowTrans_Click(object sender, EventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
            Recolor();
        }

        private void buttonCategoryMan_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.ShowDialog();
        }

        private void buttonUserMan_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.ShowDialog();
        }

        private void buttonProductMan_Click(object sender, EventArgs e)
        {
            ProductMenu product = new ProductMenu();
            product.ShowDialog();
        }

        private void buttonStaffTot_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonSalesTot_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonPaymentMan_Click(object sender, EventArgs e)
        {
            Payments payments = new Payments();
            payments.ShowDialog();
        }
    }
}
