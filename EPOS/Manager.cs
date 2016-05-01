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
            if (Globals.UserLevel != "Manager")
            {
                MessageBox.Show("Insufficient Authority");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to end the days session?",
                          "Close Till?", MessageBoxButtons.YesNo);
                switch (dr)
                {
                    case DialogResult.Yes:
                        Printer.OpenTill();
                        Printer.stafftotals();
                        Printer.salestotals();
                        SqlConnection cat = new SqlConnection(Globals.dataconnection);
                        cat.Open();
                        SqlCommand delete = new SqlCommand("DELETE FROM Transactions", cat);
                        delete.ExecuteNonQuery();
                        cat.Close();
                        SqlConnection cat2 = new SqlConnection(Globals.dataconnection);
                        cat2.Open();
                        SqlCommand delete2 = new SqlCommand("DELETE FROM ClockIn", cat2);
                        delete2.ExecuteNonQuery();
                        cat2.Close();
                        Application.Exit();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }

        private void buttonNoSale_Click(object sender, EventArgs e)
        {
            Printer.OpenTill();
            Globals.AddNoSale();

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
            ViewTabs viewtabs = new ViewTabs();
            viewtabs.ShowDialog();
        }

        private void buttonShowTrans_Click(object sender, EventArgs e)
        {
            ViewTransactions viewtrans = new ViewTransactions();
            viewtrans.ShowDialog();
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
            Productman product = new Productman();
            product.ShowDialog();
        }

        private void buttonStaffTot_Click(object sender, EventArgs e)
        {
            Printer.stafftotals();
        }

        private void buttonSalesTot_Click(object sender, EventArgs e)
        {
            Printer.salestotals();
        }

        private void buttonPaymentMan_Click(object sender, EventArgs e)
        {
            Payments payments = new Payments();
            payments.ShowDialog();
        }

        private void buttonButtonMan_Click(object sender, EventArgs e)
        {
            Globals.MenuNo = 1;
            Buttons buttons = new Buttons();
            buttons.ShowDialog();
        }
    }
}
