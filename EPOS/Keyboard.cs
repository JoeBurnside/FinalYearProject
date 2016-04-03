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
    public partial class Keyboard : Form
    {
        public string Capital;
        public Keyboard()
        {
            InitializeComponent();
        }


        private void Keyboard_Load(object sender, EventArgs e)
        {
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            textBox1.Text = Globals.KeyboardString;
            Recolor();
            if (textBox1.Text.Length < 1)
            {
                MakeBig();
            }
        }
        private void MakeBig()
        {
            Capital = "Big";
            buttonA.Text = "A";
            buttonB.Text = "B";
            buttonC.Text = "C";
            buttonD.Text = "D";
            buttonE.Text = "E";
            buttonF.Text = "F";
            buttonG.Text = "G";
            buttonH.Text = "H";
            buttonI.Text = "I";
            buttonJ.Text = "J";
            buttonK.Text = "K";
            buttonL.Text = "L";
            buttonM.Text = "M";
            buttonN.Text = "N";
            buttonO.Text = "O";
            buttonP.Text = "P";
            buttonQ.Text = "Q";
            buttonR.Text = "R";
            buttonS.Text = "S";
            buttonT.Text = "T";
            buttonU.Text = "U";
            buttonV.Text = "V";
            buttonW.Text = "W";
            buttonX.Text = "X";
            buttonY.Text = "Y";
            buttonZ.Text = "Z";
        }
        private void MakeSmall()
        {
            Capital = "Small";
            buttonA.Text = "a";
            buttonB.Text = "b";
            buttonC.Text = "c";
            buttonD.Text = "d";
            buttonE.Text = "e";
            buttonF.Text = "f";
            buttonG.Text = "g";
            buttonH.Text = "h";
            buttonI.Text = "i";
            buttonJ.Text = "j";
            buttonK.Text = "k";
            buttonL.Text = "l";
            buttonM.Text = "m";
            buttonN.Text = "n";
            buttonO.Text = "o";
            buttonP.Text = "p";
            buttonQ.Text = "q";
            buttonR.Text = "r";
            buttonS.Text = "s";
            buttonT.Text = "t";
            buttonU.Text = "u";
            buttonV.Text = "v";
            buttonW.Text = "w";
            buttonX.Text = "x";
            buttonY.Text = "y";
            buttonZ.Text = "z";
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

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 1)
            { textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1); }
            if (textBox1.Text.Length < 1)
            {
                MakeBig();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "1";
            MakeSmall();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "2";
            MakeSmall();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "3";
            MakeSmall();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "4";
            MakeSmall();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "5";
            MakeSmall();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "6";
            MakeSmall();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "7";
            MakeSmall();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "8";
            MakeSmall();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "9";
            MakeSmall();

        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "0";
            MakeSmall();
        }

        private void buttonQ_Click(object sender, EventArgs e)
        {
            if(Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "Q";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "q";
            }
        }

        private void buttonW_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "W";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "w";
            }
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "E";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "e";
            }
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "R";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "r";
            }
        }

        private void buttonT_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "T";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "t";
            }
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "Y";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "y";
            }
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "U";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "u";
            }
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "I";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "i";
            }
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "O";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "o";
            }
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "P";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "p";
            }
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "A";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "a";
            }
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "S";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "s";
            }
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "D";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "d";
            }
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "F";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "f";
            }
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "G";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "g";
            }
        }

        private void buttonH_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "H";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "h";
            }
        }

        private void buttonJ_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "J";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "j";
            }
        }

        private void buttonK_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "K";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "k";
            }
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "L";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "l";
            }
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "Z";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "z";
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "X";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "x";
            }
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "C";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "c";
            }
        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "V";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "v";
            }
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "B";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "b";
            }
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "N";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "n";
            }
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                textBox1.Text = textBox1.Text + "M";
                MakeSmall();
            }
            else
            {
                textBox1.Text = textBox1.Text + "m";
            }
        }

        private void buttonSpace_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + " ";
            MakeBig();
        }

        private void buttonShift_Click(object sender, EventArgs e)
        {
            if (Capital == "Big")
            {
                MakeSmall();
            }
            else
            {
                MakeBig();
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Globals.KeyboardString = textBox1.Text;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
