using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EPOS
{
    class Printer
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false;

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            dwCount = szString.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            SendBytesToPrinter("EPOSPrinter", pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
        public static void OpenTill()
        {
            string output = (char)27 + "p" + (char)0 + (char)25 + (char)250;
            Printer.SendStringToPrinter(output);
        }
        public static void Receipt(int idno)
        {
            Globals.Total = 0m;
            decimal discount = 0m;
            string payment = "";
            String printerstring = Printer.header;
            printerstring += (char)27 + "D" + (char)27 + (char)0;
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, SaleUnit.Price, Payment.Name, Transactions.Discount FROM Unit, SaleUnit, Product, TransactionItem, Transactions, Payment WHERE Unit.UnitID = SaleUnit.UnitID AND Product.ProductID = SaleUnit.ProductID AND SaleUnit.SaleUnitID = TransactionItem.SaleUnitID AND TransactionItem.TransactionID = Transactions.TransactionID AND Payment.PaymentID = Transactions.PaymentID AND Transactions.TransactionID = @id", basket);
            basket.Open();
            show.Parameters.AddWithValue("@id", idno);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                printerstring += reader.GetString(0) + ": ";
                printerstring += reader.GetString(1) + HorizontalTab;
                printerstring += reader.GetDecimal(2).ToString() + Newline;
                Globals.Total += reader.GetDecimal(2);
                discount = reader.GetDecimal(4);
                payment = reader.GetString(3);
            }
            basket.Close();
            Globals.Total -= discount;
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            if (discount > 0m)
            {
                printerstring += "Discount: " + HorizontalTab + discount.ToString() + Newline;
            }
            printerstring += "Total: " + HorizontalTab + Globals.Total + Newline;
            printerstring += titletext + "                            ";
            printerstring += Newline + normaltext + Centre + "Paid By " + payment;
            printerstring += footer;
            SendStringToPrinter(printerstring);
        }
        public static void Bill(int idno)
        {
            Globals.Total = 0m;
            String printerstring = Printer.header;
            printerstring += (char)27 + "D" + (char)27 + (char)0;
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, SaleUnit.Price FROM Unit, SaleUnit, Product, TabItem, Tab WHERE Unit.UnitID = SaleUnit.UnitID AND Product.ProductID = SaleUnit.ProductID AND SaleUnit.SaleUnitID = TabItem.SaleUnitID AND TabItem.TabID = Tab.TabID AND Tab.TabID = @id", basket);
            basket.Open();
            show.Parameters.AddWithValue("@id", idno);
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                printerstring += reader.GetString(0) + ": ";
                printerstring += reader.GetString(1) + HorizontalTab;
                printerstring += reader.GetDecimal(2).ToString() + Newline;
                Globals.Total += reader.GetDecimal(2);


            }
            basket.Close();
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            printerstring += "Total: " + HorizontalTab + Globals.Total + Newline;
            printerstring += titletext + "                            ";
            printerstring += footer;
            SendStringToPrinter(printerstring);
        }
        public static void salestotals()
        {
            Globals.Total = 0m;
            decimal totsales = 0m;
            String printerstring = header;
            printerstring += (char)27 + "D" + (char)27 + (char)0;
            printerstring += normaltext + Centre + "Sales";
            printerstring += Newline;
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("SELECT Product.Name, Unit.Name, Count(TransactionItem.SaleUnitID) AS Quantity, SUM(SaleUnit.Price) AS Price FROM ((((SaleUnit INNER JOIN Product ON Product.ProductID = SaleUnit.ProductID) INNER JOIN Unit ON Unit.UnitID = SaleUnit.UnitID) INNER JOIN TransactionItem ON TransactionItem.SaleUnitID = SaleUnit.SaleUnitID) INNER JOIN Transactions ON Transactions.TransactionID = TransactionItem.TransactionID) WHERE Transactions.Type = @Type GROUP BY Product.Name, Unit.Name ORDER BY Product.Name, Unit.Name", basket);
            basket.Open();
            show.Parameters.AddWithValue("@Type", "Sale");
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {
                printerstring += Left + reader.GetString(0) + ": ";
                printerstring += reader.GetString(1) + Newline;
                int quant = reader.GetInt32(2);
                printerstring += "Quantity: " + quant.ToString() + HorizontalTab;
                decimal price = reader.GetDecimal(3);
                printerstring += price.ToString() + Newline;
                Globals.Total += price;
                totsales += price;
            }
            basket.Close();
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            printerstring += normaltext + Centre + "Refunds";
            printerstring += Newline;
            decimal totref = 0m;
            SqlConnection basket3 = new SqlConnection(Globals.dataconnection);
            SqlCommand show3 = new SqlCommand("SELECT Product.Name, Unit.Name, Count(TransactionItem.SaleUnitID) AS Quantity, SUM(SaleUnit.Price) AS Price FROM ((((SaleUnit INNER JOIN Product ON Product.ProductID = SaleUnit.ProductID) INNER JOIN Unit ON Unit.UnitID = SaleUnit.UnitID) INNER JOIN TransactionItem ON TransactionItem.SaleUnitID = SaleUnit.SaleUnitID) INNER JOIN Transactions ON Transactions.TransactionID = TransactionItem.TransactionID) WHERE Transactions.Type = @Type GROUP BY Product.Name, Unit.Name ORDER BY Product.Name, Unit.Name", basket3);
            basket3.Open();
            show3.Parameters.AddWithValue("@Type", "Refund");
            SqlDataReader reader3 = show3.ExecuteReader();
            while (reader3.Read())
            {
                printerstring += Left + reader3.GetString(0) + ": ";
                printerstring += reader3.GetString(1) + Newline;
                int quant = reader3.GetInt32(2);
                printerstring += "Quantity: " + quant.ToString() + HorizontalTab;
                decimal price = reader3.GetDecimal(3);
                printerstring += price.ToString() + Newline;
                Globals.Total -= price;
                totref += price;
            }
            basket3.Close();
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            decimal discount = 0m;
            SqlConnection basket2 = new SqlConnection(Globals.dataconnection);
            SqlCommand show2 = new SqlCommand("SELECT Discount FROM Transactions", basket2);
            basket2.Open();
            SqlDataReader reader2 = show2.ExecuteReader();
            while (reader2.Read())
            {
                if (!reader2.IsDBNull(0))
                {
                    discount += reader2.GetDecimal(0);
                }
            }
            basket2.Close();
            printerstring += "Total Sales: " + HorizontalTab + totsales.ToString() + Newline;
            if (totref > 0m)
            {
                printerstring += "Refunds: " + HorizontalTab + totref.ToString() + Newline;
            }
            if (discount > 0m)
            {
                printerstring += "Discounts: " + HorizontalTab + discount.ToString() + Newline;
                Globals.Total -= discount;
            }
            printerstring += "Total: " + HorizontalTab + Globals.Total + Newline;
            printerstring += titletext + "                            ";
            printerstring += footer;
            SendStringToPrinter(printerstring);
        }
        public static void stafftotals()
        {
            String printerstring = header;
            printerstring += (char)27 + "D" + (char)27 + (char)0;
            printerstring += normaltext + Centre + "Staff Totals";
            printerstring += Newline;
            SqlConnection basket = new SqlConnection(Globals.dataconnection);
            SqlCommand show = new SqlCommand("Select Staff.Name, Transactions.Type, SUM(SaleUnit.Price) FROM (Staff INNER JOIN Transactions ON Staff.StaffID = Transactions.StaffID), TransactionItem, SaleUnit WHERE Transactions.TransactionID = TransactionItem.TransactionID AND TransactionItem.SaleUnitID = SaleUnit.SaleUnitID GROUP BY Staff.Name, Transactions.Type", basket);
            basket.Open();
            string name = "";
            show.Parameters.AddWithValue("@Type", "Sale");
            SqlDataReader reader = show.ExecuteReader();
            while (reader.Read())
            {

                if (reader.GetString(0) != name)
                {
                    printerstring += titletext + "                            " + Newline + Newline + normaltext;
                    printerstring += normaltext + Centre + reader.GetString(0) + Newline;
                }
                printerstring += (char)27 + "D" + (char)27 + (char)0;
                printerstring += Left + reader.GetString(1) + ": ";
                printerstring += HorizontalTab;
                if (!reader.IsDBNull(2))
                {
                    printerstring += reader.GetDecimal(2).ToString() + Newline;
                }
                else
                {
                    printerstring += Newline;
                }
                name = reader.GetString(0);

            }
            basket.Close();
            printerstring += (char)27 + "D" + (char)27 + (char)0;
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            printerstring += normaltext + Centre + "Transaction Totals";
            printerstring += Newline;
            SqlConnection basket3 = new SqlConnection(Globals.dataconnection);
            SqlCommand show3 = new SqlCommand("Select Staff.Name, Transactions.Type, COUNT(Transactions.TransactionID) FROM (Staff INNER JOIN Transactions ON Staff.StaffID = Transactions.StaffID) GROUP BY Staff.Name, Transactions.Type", basket3);
            basket3.Open();
            string name2 = "";
            show3.Parameters.AddWithValue("@Type", "Sale");
            SqlDataReader reader3 = show3.ExecuteReader();
            while (reader3.Read())
            {

                if (reader3.GetString(0) != name2)
                {
                    printerstring += titletext + "                            " + Newline + Newline + normaltext;
                    printerstring += normaltext + Centre + reader3.GetString(0) + Newline;
                }
                printerstring += (char)27 + "D" + (char)27 + (char)0;
                printerstring += Left + reader3.GetString(1) + ": ";
                printerstring += HorizontalTab;
                if (!reader3.IsDBNull(2))
                {
                    printerstring += reader3.GetInt32(2).ToString() + Newline;
                }
                else
                {
                    printerstring += Newline;
                }
                name2 = reader3.GetString(0);

            }
            basket3.Close();
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            printerstring += Centre + "Clock" + Newline;
            printerstring += titletext + "                            " + Newline + Newline + normaltext;
            SqlConnection basket2 = new SqlConnection(Globals.dataconnection);
            SqlCommand show2 = new SqlCommand("SELECT Staff.Name, ClockIn.InOrOut, ClockIn.Time FROM ClockIn, Staff WHERE Staff.StaffID = ClockIn.StaffID ORDER BY Staff.Name, ClockIn.Time", basket2);
            basket2.Open();
            SqlDataReader reader2 = show2.ExecuteReader();
            while (reader2.Read())
            {
                printerstring += reader2.GetString(0) + HorizontalTab;
                printerstring += reader2.GetString(1) + Newline;
                printerstring += reader2.GetDateTime(2).ToString() + Newline + Newline;
            }
            basket2.Close();
            printerstring += titletext + "                            ";
            printerstring += footer;
            SendStringToPrinter(printerstring);
        }
        public const char Escape = (char)27;
        public const char Newline = (char)10;
        public const char HorizontalTab = (char)0x09;
        public const char VerticalTab = (char)0x0b;
        public static string Left = Escape + "a" + (char)0;
        public static string Centre = Escape + "a" + (char)1;
        public static string Right = Escape + "a" + (char)2;
        public static string High = Escape + "!" + (char)16;
        public static string Regular = Escape + "!" + (char)0;
        public static string Underline = Escape + "-" + (char)1;
        public static string Nounderline = Escape + "-" + (char)0;
        public static string titletext = Centre + High + Underline;
        public static string normaltext = Left + Regular + Nounderline;
        public static string title = Escape + "@" + titletext + Globals.Pubname;
        public static string header = title + Newline + Newline + Regular + "Server: " + Globals.Username + Newline + DateTime.Now + Newline + titletext + "                            " + Newline + Newline + normaltext;
        public static string footer = Escape + "d" + (char)5;

    }
}
