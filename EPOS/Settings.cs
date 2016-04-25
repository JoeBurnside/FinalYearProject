using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS
{
    public partial class Settings : Form
    { 
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            Recolor();
            fillboxes();
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

        private void fillboxes()
        {
            comboBoxBg.DrawItem += new DrawItemEventHandler(comboBoxBg_DrawItem);
            comboBoxButton.DrawItem += new DrawItemEventHandler(comboBoxButton_DrawItem);
            comboBoxFont.DrawItem += new DrawItemEventHandler(comboBoxFont_DrawItem);
            Type t = typeof(Color);
                PropertyInfo[] p = t.GetProperties();

                foreach (PropertyInfo item in p)
                {
                    if (item.PropertyType.FullName.Equals("System.Drawing.Color", StringComparison.CurrentCultureIgnoreCase))
                    {
                    comboBoxBg.Items.Add(item.Name);
                    comboBoxButton.Items.Add(item.Name);
                    comboBoxFont.Items.Add(item.Name);
                    }
                }
            comboBoxBg.SelectedItem = Globals.Backcolor;
            comboBoxButton.SelectedItem = Globals.Buttoncolor;
            comboBoxFont.SelectedItem = Globals.Fontcolor;
            textBoxName.Text = Globals.Pubname;
                }
        private void comboBoxBg_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = comboBoxBg.Font;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 350, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }
        private void comboBoxButton_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = comboBoxButton.Font;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 350, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }
        private void comboBoxFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = comboBoxFont.Font;
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 350, rect.Y + 5,
                                rect.Width - 10, rect.Height - 10);
            }
        }
        private Brush GetCurrentBrush(string colorName)
        {
            return new SolidBrush(Color.FromName(colorName));
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

        private void comboBoxBg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DrawColor(DrawItemEventArgs e, Color color, ref int nextX)
        {
            int width = e.Bounds.Height * 2 - 8;
            Rectangle rectangle = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, width, e.Bounds.Height - 6);
            e.Graphics.FillRectangle(new SolidBrush(color), rectangle);

            nextX = width + 8;
        }
        private void DrawText(DrawItemEventArgs e, Color color, int nextX)
        {
            e.Graphics.DrawString(color.Name, e.Font, new SolidBrush(e.ForeColor), new PointF(nextX, e.Bounds.Y + (e.Bounds.Height - e.Font.Height) / 2));
        }

        private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void comboBoxButton_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text != "")
            {
                if (textBoxName.Text.Length < 255)
                {
                    String Pubname = textBoxName.Text;
                    String Backcolor = comboBoxBg.SelectedItem.ToString();
                    String Buttoncolor = comboBoxButton.SelectedItem.ToString();
                    String Fontcolor = comboBoxFont.SelectedItem.ToString();
                    using (StreamWriter writer =
                new StreamWriter(@"C:\Users\Joe\Desktop\Uni Stuff\FYP\EPOS\EPOS\Settings.txt"))
                    {
                        writer.WriteLine(Pubname);
                        writer.WriteLine(Fontcolor);
                        writer.WriteLine(Backcolor);
                        writer.WriteLine(Buttoncolor);
                    }
                    Manager login = new Manager();
                    login.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Pub name is too long");
                }
            }
            else
            {
                MessageBox.Show("Please enter a pub name");
            }
        }
    }
}
