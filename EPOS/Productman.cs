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
    public partial class Productman : Form
    {
        public Productman()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            Units units = new Units();
            units.ShowDialog();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.ShowDialog();
        }

        private void labelDate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelName_Click(object sender, EventArgs e)
        {

        }

        private void Productman_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
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
