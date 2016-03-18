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
            ManagerLogin managerLogin = new ManagerLogin("NoSale");
            managerLogin.Show();
        }
        private void Recolor()
        {
            BackColor = Color.FromName(Globals.Backcolor);
            buttonBack.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonEnd.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonSalesTot.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonStaffTot.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonTables.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonButtonMan.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonCategoryMan.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonProductMan.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonUserMan.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonPaymentMan.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonShowTrans.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonSettings.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonNoSale.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonBack.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonEnd.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonSalesTot.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonStaffTot.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonTables.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonButtonMan.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonCategoryMan.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonProductMan.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonUserMan.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonPaymentMan.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonShowTrans.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonSettings.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonNoSale.ForeColor = Color.FromName(Globals.Fontcolor);
            labelDate.ForeColor = Color.FromName(Globals.Fontcolor);
            labelName.ForeColor = Color.FromName(Globals.Fontcolor);
            labelName.Text = Globals.Pubname;
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
