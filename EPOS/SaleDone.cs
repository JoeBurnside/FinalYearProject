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
    public partial class SaleDone : Form
    {
        public SaleDone()
        {
            InitializeComponent();
        }

        private void SaleDone_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            labelChange.Text = "£" + Globals.Change;
        }
        private void Recolor()
        {
            Globals.GetColors();
            BackColor = Color.FromName(Globals.Backcolor);
            labelName.Text = Globals.Pubname;
            labelStaffName.Text = "Server: " + Globals.Username;
            foreach (Control c in this.Controls)
            {
                Globals.UpdateColorControls(c);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReceipt_Click(object sender, EventArgs e)
        {
            Printer.Receipt(Globals.IDNo);
        }
    }
}
