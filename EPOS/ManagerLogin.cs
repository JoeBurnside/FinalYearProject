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
    public partial class ManagerLogin : Form
    {
        public ManagerLogin(string directTo)
        {
            InitializeComponent();
            this._directTo = directTo;
        }

        private string _directTo = String.Empty;

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "2";
        }

        

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            switch (this._directTo)
            {
                case "ManagerMenu":
                    if (textBoxCode.Text != "")
                    {
                        Globals.TryLogin(textBoxCode.Text);
                        if (Globals.UserLevel == "Manager" || Globals.UserLevel == "Supervisor")
                        {
                            Manager managermenu = new Manager();
                            managermenu.ShowDialog();
                        }
                        else if (Globals.UserLevel == "Staff")
                        {
                            MessageBox.Show("Insufficient Authority");
                        }
                        else if (Globals.UserLevel == "")
                        {
                            MessageBox.Show("Incorrect Login Code");
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Login Code");
                    }
                    break;
                case "NoSale":
                    if (textBoxCode.Text != "")
                    {
                        Globals.TryLogin(textBoxCode.Text);
                        if (Globals.UserLevel == "Manager" || Globals.UserLevel == "Supervisor")
                        {
                            Application.Exit();
                        }
                        else if (Globals.UserLevel == "Staff")
                        {
                            MessageBox.Show("Insufficient Authority");
                        }
                        else if (Globals.UserLevel == "")
                        {
                            MessageBox.Show("Incorrect Login Code");
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Login Code");
                    }
                    break;
                case "ClockIn":
                    if (textBoxCode.Text != "")
                    {
                        Globals.TryLogin(textBoxCode.Text);
                        if (Globals.Username != "")
                        {
                            int ClockIn = Globals.CheckClock(Globals.Userno, "In");
                            int ClockOut = Globals.CheckClock(Globals.Userno, "Out");
                            if (ClockIn > 0)
                            {
                                if (ClockOut == ClockIn)
                                {
                                    AddClock(Globals.Userno, "In");
                                    MessageBox.Show("You have clocked in at " + DateTime.Now.ToString("HH:mm:ss"));
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("You have already Clocked In");
                                    this.Close();
                                }
                            }
                            else
                            {
                                AddClock(Globals.Userno, "In");
                                MessageBox.Show("You have clocked in at " + DateTime.Now.ToString("HH:mm:ss"));
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Login Code");
                            this.Close();
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Login Code");
                    }
                    
                    break;
                case "ClockOut":
                    if (textBoxCode.Text != "")
                    {
                        Globals.TryLogin(textBoxCode.Text);
                        if (Globals.Username != "")
                        {
                            int ClockIn = Globals.CheckClock(Globals.Userno, "In");
                            int ClockOut = Globals.CheckClock(Globals.Userno, "Out");
                            if (ClockIn > 0)
                            {
                                if (ClockOut == ClockIn)
                                {
                                    MessageBox.Show("You are already Clocked Out");
                                    this.Close();
                                }
                                else
                                {
                                    AddClock(Globals.Userno, "Out");
                                    MessageBox.Show("You have clocked out at " + DateTime.Now.ToString("HH:mm:ss"));
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("You are not Clocked In");
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Login Code");
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Enter Login Code");
                    }

                    break;
                default:
                    MessageBox.Show("Error");
                    break;
            }
        }

        private void ManagerLogin_Load(object sender, EventArgs e)
        {
              this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBoxCode.Text = textBoxCode.Text + "1";
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

        private void buttonDel_Click_1(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Length >= 1)
            { textBoxCode.Text = textBoxCode.Text.Remove(textBoxCode.Text.Length - 1); }
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToString("MMMM dd, yyyy") + Environment.NewLine + DateTime.Now.ToString("HH:mm:ss");
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
        private void AddClock(string StaffID, string InOrOut)
        {
            SqlConnection clockin = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("INSERT INTO ClockIn (StaffID, InOrOut, Time) VALUES (@id, @in, @time)", clockin);
            clockin.Open();
            show.Parameters.AddWithValue("@id", StaffID);
            show.Parameters.AddWithValue("@in", InOrOut);
            show.Parameters.AddWithValue("@time", DateTime.Now);
            show.ExecuteNonQuery();
            clockin.Close();
        }
    }
}
