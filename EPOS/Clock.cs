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
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void Clock_Load(object sender, EventArgs e)
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

        private void buttonIn_Click(object sender, EventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin("ClockIn");
            managerLogin.ShowDialog();
            this.Close();
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin("ClockOut");
            managerLogin.ShowDialog();
            this.Close();
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
    }
}
