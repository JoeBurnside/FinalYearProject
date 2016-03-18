using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS
{
    class Globals
    {
        private static string m_pubname;
        public static string Pubname
        {
            get { return m_pubname; }
            set { m_pubname = value; }
        }
        private static string m_backcolor = "Red";
        public static string Backcolor
        {
            get { return m_backcolor; }
            set { m_backcolor = value; }
        }
        private static string m_fontcolor = "Blue";
        public static string Fontcolor
        {
            get { return m_fontcolor; }
            set { m_fontcolor = value; }
        }
        private static string m_buttoncolor = "Green";
        public static string Buttoncolor
        {
            get { return m_buttoncolor; }
            set { m_buttoncolor = value; }
        }
    }
}
