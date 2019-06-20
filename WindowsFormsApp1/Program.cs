using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
//using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;
using QRCoder;


namespace WindowsFormsApp1
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static string val;
        static double amt;
        static int xmlRecordCount;
        static int xmlLastNumber;
        static string xmlURL;
        static string xmlEnableQRCode;
        static FileSystemWatcher _watcher;
        //FileMonitor fm = new FileMonitor();
        //private static string xmlFile = @"D:\PointSoft Dn\Probation Project\CTP.xml";
        //private static FileStream fs = new FileStream(@"D:\PointSoft Dn\Probation Project\mcb.txt", FileMode.OpenOrCreate, FileAccess.Write);
        //private static StreamWriter m_streamWriter = new StreamWriter(fs);
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateFileWatcher(@"D:\PointSoft Dn\Probation Project\20190321\");
            //Init();
            Application.Run(new Form1());
        }

        //static void Init()
        //{
        //    string directory = @"D:\PointSoft Dn\Probation Project\20190321\";
        //    Program._watcher = new FileSystemWatcher(directory);
        //    Program._watcher.Changed +=
        //        new FileSystemEventHandler(Program._watcher_Changed);
        //    Program._watcher.EnableRaisingEvents = true;
        //    Program._watcher.IncludeSubdirectories = true;
        //}


        /// <summary>
        /// Handler.
        /// </summary>
        //static void _watcher_Changed(object sender, FileSystemEventArgs e)
        //{
            //MessageBox.Show("CHANGED, NAME: " + e.Name);
            //MessageBox.Show("CHANGED, FULLPATH: " + e.FullPath);
            // Can change program state (set invalid state) in this method.
            // ... Better to use insensitive compares for file names.

            //PrintPreviewDialog1.Document = printDocument1;
            //printDocument1.PrintPage += new System.Srawing.Printing.PrintPageEventHandler(PrintPage);
            //PrintPreviewDialog1.Show();
            //printPreviewControl1.Document = printDocument1;
        //}
        //public static DataTable GetDataTable()
        //{
        //    //OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");
        //    OleDbConnection dbfConnectionHandler = Odbcon();

        //    if (dbfConnectionHandler.State == ConnectionState.Open)
        //    {
        //        OleDbCommand oCmd = dbfConnectionHandler.CreateCommand();
        //        oCmd.CommandText = @"SELECT * FROM CTP.dbf WHERE NUMBER < 999999";
        //        DataTable dt = new DataTable();
        //        dt.Load(oCmd.ExecuteReader());
        //        dbfConnectionHandler.Close();
        //        return dt;
        //    }
        //    return null;
        //}

        //private static OleDbConnection DbfConnection(string path)
        //{
        //    OleDbConnection oleConnectHandle = new OleDbConnection(
        //        @"Provider=VFPOLEDB.1;Data Source=" + path);
        //    oleConnectHandle.Open();

        //    return oleConnectHandle;
        //}

        //public static OleDbConnection Odbcon()
        //{
        //    OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");

        //    return dbfConnectionHandler;
        //}

        //public DataTable CreateDataTable(string s, string s2)
        //{
        //    //OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");
        //    OleDbConnection dbfConnectionHandler = Odbcon();


        //    if (dbfConnectionHandler.State == ConnectionState.Open)
        //    {
        //        OleDbCommand oCmd = dbfConnectionHandler.CreateCommand();
        //        oCmd.CommandText = @"INSERT INTO CTP.dbf "+s+" VALUES(" +s2;

        //        DataTable dt = new DataTable();
        //        oCmd.ExecuteReader();
        //        //dt.Load(oCmd.ExecuteReader());
        //        //dbfConnectionHandler.Close();
        //        return null;
        //    }
        //    return null;
        //}


        public static void CreateFileWatcher(string path)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "CTP.dbf";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            FileMonitor fileMon = new FileMonitor();
            fileMon.OnFileChange();
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            MessageBox.Show($"File: {e.ChangeType} renamed to {e.FullPath}"); //("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }


        //public static void OnFileChange()
        //{
        //    DataTable dt = new DataTable();
        //    dt = GetDataTable();
        //    int numOfBill = 0;

        //    int latestRecCount = dt.Rows.Count;

        //    if (File.Exists(xmlFile))
        //    {
        //        ProcessXML(xmlFile);

        //        if (latestRecCount > xmlRecordCount)
        //        {

        //            for (var n = xmlRecordCount; n < dt.Rows.Count; n++)
        //            {
        //                val = "Bill for " + dt.Rows[n]["Shopcode"].ToString() + " :";
        //                val += dt.Rows[n]["Date"].ToString() + " " + dt.Rows[n]["Time"].ToString();

        //                if (Convert.ToInt32(dt.Rows[n]["Number"]) > xmlLastNumber) //Last Number = 5504 xmlLastNumber
        //                {
        //                    amt += Convert.ToDouble(dt.Rows[n]["Amount"]);
        //                    val += " : " + dt.Rows[n]["Amount"].ToString() + " :" + amt.ToString();
        //                    m_streamWriter.WriteLine(val);
        //                    m_streamWriter.Flush();
        //                    numOfBill += 1;
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //            CreateConfigurationFile(dt, recCount: latestRecCount, number: Convert.ToInt32(dt.Rows[xmlRecordCount]["Number"]), update: true);

        //            m_streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), latestRecCount);
        //            m_streamWriter.Flush();

        //            if ((numOfBill >= 1 || amt >= 50) && xmlEnableQRCode == "Enable")
        //            {
        //                PrintQRCode(val);
        //            }

        //            //NotifyIcon1.Icon = SystemIcons.Application;
        //            //NotifyIcon1.Visible = true;
        //            //NotifyIcon1.BalloonTipText = DateTime.Now.ToLongTimeString();
        //            //NotifyIcon1.ShowBalloonTip(1000);
        //        }
        //    }
        //}

        //public static void CreateConfigurationFile(DataTable dt, long? recCount = null, int? number = null, string url = "", bool? enableQRCode = true, bool? update = false)
        //{
        //    if (update == false)
        //    {
        //        XmlWriterSettings settings = new XmlWriterSettings
        //        {
        //            Indent = true,
        //            IndentChars = ("    "),
        //            CloseOutput = true,
        //            OmitXmlDeclaration = true
        //        };
        //        using (XmlWriter writer = XmlWriter.Create(xmlFile, settings))
        //        {

        //            writer.WriteStartElement("root");
        //            writer.WriteElementString("recordcount", recCount.ToString());
        //            writer.WriteElementString("url", url);

        //            if (enableQRCode == true)
        //            {
        //                writer.WriteElementString("enableQRCode", "Enable");
        //            }
        //            else
        //            {
        //                writer.WriteElementString("enableQRCode", "Disable");
        //            }

        //            for (var i = 0; i < dt.Columns.Count; i++)
        //            {
        //                writer.WriteElementString(dt.Columns[i].ToString(), dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString());
        //            }
        //            writer.WriteEndElement();
        //            writer.Flush();
        //        }
        //    }
        //    else
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(xmlFile);
        //        //doc.Load(@"C: \Users\HBTan\source\repos\PsRetailer\WindowsFormsApp1\App.config");

        //        var idElement = doc.GetElementsByTagName("recordcount");

        //        if (recCount != null && idElement != null)
        //        {
        //            idElement[0].FirstChild.Value = recCount.ToString();
        //        }

        //        if (url != "")
        //        {
        //            idElement = doc.GetElementsByTagName("url");
        //            idElement[0].FirstChild.Value = url;
        //        }

        //        if (enableQRCode != null)
        //        {
        //            idElement = doc.GetElementsByTagName("enableQRCode");

        //            if (enableQRCode == true)
        //            {
        //                idElement[0].FirstChild.Value = "Enable";
        //            }
        //            else
        //            {
        //                idElement[0].FirstChild.Value = "Disable";
        //            }

        //        }

        //        if (dt != null)
        //        {
        //            for (var i = 0; i < dt.Columns.Count; i++)
        //            {
        //                idElement = doc.GetElementsByTagName(dt.Columns[i].ToString());

        //                if (dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString() != null)
        //                {
        //                    idElement[0].InnerText = dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString();
        //                }
        //            }
        //        }
        //        doc.Save(xmlFile);
        //    }
        //}


        //public static void AssignValue(string recCount, string lastNumber, string url, string enableQRCode)
        //{
        //    xmlRecordCount = Convert.ToInt32(recCount);
        //    xmlLastNumber = Convert.ToInt32(lastNumber);
        //    xmlURL = url;
        //    xmlEnableQRCode = enableQRCode;
        //}


        //public static string ProcessXML(string xmlText)
        //{
        //    Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(xmlText);
        //    XmlNodeList _recordcount = doc.GetElementsByTagName("recordcount");
        //    XmlNodeList number = doc.GetElementsByTagName("number");
        //    XmlNodeList connection_path = doc.GetElementsByTagName("connection_path");
        //    XmlNodeList filename = doc.GetElementsByTagName("filename");
        //    XmlNodeList url = doc.GetElementsByTagName("url");
        //    XmlNodeList enableQRCode = doc.GetElementsByTagName("enableQRCode");

        //    for (int i = 0; i < _recordcount.Count; ++i)
        //    {
        //        AssignValue(_recordcount[i].InnerText,
        //        number[i].InnerText,
        //        url[i].InnerText,
        //        enableQRCode[i].InnerText);
        //        xmlEnableQRCode = enableQRCode[i].InnerText;
        //    }
        //    return xmlEnableQRCode;
        //}



        //public static void PrintQRCode(string text1)
        //{
        //    Form2 f2 = new Form2();
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(text1, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(6);
        //    f2.pictureBox1.Image = qrCodeImage;
        //    f2.pictureBox1.Visible = true;
        //    f2.Show();
        //}
    }
}
