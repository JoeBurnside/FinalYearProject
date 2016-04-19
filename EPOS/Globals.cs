using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOS
{
    class Globals
    {
        public static string dataconnection = @"Data Source=(localdb)\Projectsv12;Initial Catalog=EposData;Integrated Security=True";
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
        public static void GetColors() {
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
        private static string m_userlevel;
        public static string UserLevel
        {
            get { return m_userlevel; }
            set { m_userlevel = value; }
        }
        private static string m_username;
        public static string Username
        {
            get { return m_username; }
            set { m_username = value; }
        }
        private static string m_userno;
        public static string Userno
        {
            get { return m_userno; }
            set { m_userno = value; }
        }
        public static void AddNoSale()
        {
            SqlConnection nosale = new SqlConnection(Globals.dataconnection);
            SqlCommand insert = new SqlCommand("INSERT INTO Transactions (Type, Time, StaffID) VALUES (@type, @time, @staffid)", nosale);
            nosale.Open();
            insert.Parameters.AddWithValue("@type", "No Sale");
            insert.Parameters.AddWithValue("@time", DateTime.Now);
            insert.Parameters.AddWithValue("@staffid", Globals.Userno);
            insert.ExecuteNonQuery();
            nosale.Close();
        }
        public static void TryLogin(string id)
        {
            SqlConnection login = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT Name, Level FROM Staff WHERE StaffID=@id", login);
            login.Open();
            show.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = show.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Globals.Userno = id;
                    Globals.Username = reader[0].ToString();
                    Globals.UserLevel = reader[1].ToString();
                }
                
            }
            else
            {
                Globals.Username = "";
                Globals.UserLevel = "";
            }
            login.Close();
        }
        public static int CheckClock(String StaffID, String InOrOut)
        {
            int Clocked = 0;
            SqlConnection clockin = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT * FROM ClockIn WHERE StaffID=@id AND InOrOut=@in", clockin);
            clockin.Open();
            show.Parameters.AddWithValue("@id", StaffID);
            show.Parameters.AddWithValue("@in", InOrOut);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                Clocked++;
            }
            clockin.Close();
            return Clocked;
        }
        private static string m_keyboardstring;
        public static string KeyboardString
        {
            get { return m_keyboardstring; }
            set { m_keyboardstring = value; }
        }
        public static void UpdateColorControls(Control myControl)
        {
            if (myControl is Button)
            {
                myControl.BackColor = Color.FromName(Globals.Buttoncolor);
                myControl.ForeColor = Color.FromName(Globals.Fontcolor);
            }
            if (myControl is Label)
            {
                myControl.ForeColor = Color.FromName(Globals.Fontcolor);
            }
            if (myControl is ListBox)
            {
                myControl.BackColor = Color.FromName(Globals.Backcolor);
                myControl.ForeColor = Color.FromName(Globals.Fontcolor);
            }
        }
        private static int m_idno;
        public static int IDNo
        {
            get { return m_idno; }
            set { m_idno = value; }
        }
        private static int m_btnno;
        public static int ButtonNo
        {
            get { return m_btnno; }
            set { m_btnno = value; }
        }
        private static int m_menuno;
        public static int MenuNo
        {
            get { return m_menuno; }
            set { m_menuno = value; }
        }
    }
}
