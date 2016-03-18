﻿using System;
using System.IO;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            GetColors();
            Recolor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text+"1";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");   
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Length >= 1)
            { textBoxCode.Text = textBoxCode.Text.Remove(textBoxCode.Text.Length - 1); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "0";
        }

        private void buttonManager_Click(object sender, EventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin("ManagerMenu");
            managerLogin.Show();
        }

        private void buttonNosale_Click(object sender, EventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin("NoSale");
            managerLogin.Show();
        }

        private void buttonClockin_Click(object sender, EventArgs e)
        {
            Clock clock = new Clock();
            clock.Show();
        }

        private void Recolor()
        {
            BackColor = Color.FromName(Globals.Backcolor);
            button0.BackColor = Color.FromName(Globals.Buttoncolor);
            button1.BackColor = Color.FromName(Globals.Buttoncolor);
            button2.BackColor = Color.FromName(Globals.Buttoncolor);
            button3.BackColor = Color.FromName(Globals.Buttoncolor);
            button4.BackColor = Color.FromName(Globals.Buttoncolor);
            button5.BackColor = Color.FromName(Globals.Buttoncolor);
            button6.BackColor = Color.FromName(Globals.Buttoncolor);
            button7.BackColor = Color.FromName(Globals.Buttoncolor);
            button8.BackColor = Color.FromName(Globals.Buttoncolor);
            button9.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonDel.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonClockin.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonContinue.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonManager.BackColor = Color.FromName(Globals.Buttoncolor);
            buttonNosale.BackColor = Color.FromName(Globals.Buttoncolor);
            button0.ForeColor = Color.FromName(Globals.Fontcolor);
            button1.ForeColor = Color.FromName(Globals.Fontcolor);
            button2.ForeColor = Color.FromName(Globals.Fontcolor);
            button3.ForeColor = Color.FromName(Globals.Fontcolor);
            button4.ForeColor = Color.FromName(Globals.Fontcolor);
            button5.ForeColor = Color.FromName(Globals.Fontcolor);
            button6.ForeColor = Color.FromName(Globals.Fontcolor);
            button7.ForeColor = Color.FromName(Globals.Fontcolor);
            button8.ForeColor = Color.FromName(Globals.Fontcolor);
            button9.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonClockin.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonContinue.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonManager.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonNosale.ForeColor = Color.FromName(Globals.Fontcolor);
            buttonDel.ForeColor = Color.FromName(Globals.Fontcolor);
            label1.ForeColor = Color.FromName(Globals.Fontcolor);
            labelDate.ForeColor = Color.FromName(Globals.Fontcolor);
            labelName.ForeColor = Color.FromName(Globals.Fontcolor);
            labelName.Text = Globals.Pubname;
        }
        private void GetColors()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Joe\Desktop\Uni Stuff\FYP\EPOS\EPOS\Settings.txt");
            List<string> lines = new List<string>();
            while (!sr.EndOfStream)
            lines.Add(sr.ReadLine());
            sr.Close();

            Globals.Pubname = lines[0];
            Globals.Fontcolor = lines[1];
            Globals.Backcolor = lines[2];
            Globals.Buttoncolor = lines[3];
        }

    }
}
