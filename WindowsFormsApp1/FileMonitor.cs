using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Configuration;
using QRCoder;

namespace WindowsFormsApp1
{
    public class FileMonitor
    {
        private static string xmlFile = @"D:\PointSoft Dn\Probation Project\CTP.xml";
        private static FileStream fs = new FileStream(@"D:\PointSoft Dn\Probation Project\mcb.txt", FileMode.OpenOrCreate, FileAccess.Write);
        private static StreamWriter m_streamWriter = new StreamWriter(fs);
        string val;
        static double amt;
        static int xmlRecordCount;
        static int xmlLastNumber;
        static string xmlURL;
        static string xmlEnableQRCode;
        string num;
        private Dictionary<InfoKey, string> xmlInfo;

        //public void CreateFileWatcher(string path)
        //{
        //    // Create a new FileSystemWatcher and set its properties.
        //    FileSystemWatcher watcher = new FileSystemWatcher();
        //    watcher.Path = path;
        //    /* Watch for changes in LastAccess and LastWrite times, and 
        //       the renaming of files or directories. */
        //    //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
        //    //   | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        //    // Only watch text files.
        //    watcher.Filter = "CTP.dbf";

        //    // Add event handlers.
        //    watcher.Changed += new FileSystemEventHandler(OnChanged);
        //    watcher.Created += new FileSystemEventHandler(OnChanged);
        //    watcher.Deleted += new FileSystemEventHandler(OnChanged);
        //    watcher.Renamed += new RenamedEventHandler(OnRenamed);

        //    // Begin watching.
        //    watcher.EnableRaisingEvents = true;
        //}

        //private void OnChanged(object source, FileSystemEventArgs e)
        //{
        //    // Specify what is done when a file is changed, created, or deleted.
        //    OnFileChange();
        //}

        //private static void OnRenamed(object source, RenamedEventArgs e)
        //{
        //    // Specify what is done when a file is renamed.
        //    MessageBox.Show($"File: {e.ChangeType} renamed to {e.FullPath}"); //("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        //}

        private static OleDbConnection DbfConnection(string path)
        {
            OleDbConnection oleConnectHandle = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + path);
            oleConnectHandle.Open();

            return oleConnectHandle;
        }

        public static OleDbConnection Odbcon()
        {
            OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");

            return dbfConnectionHandler;
        }

        public void OnFileChange()
        {
            DataTable dt = new DataTable();
            dt = GetDataTable();
            int numOfBill = 0;
            int latestRecCount = dt.Rows.Count;

            if (File.Exists(xmlFile))
            {
                var xmlInfo = ProcessXML(xmlFile);

                if (latestRecCount > xmlRecordCount)
                {
                    for (var n = xmlRecordCount; n < dt.Rows.Count; n++)
                    {
                        val = dt.Rows[n]["Shopcode"].ToString().Trim();
                        val += Convert.ToDateTime(dt.Rows[n]["Date"]).ToString("ddMMyyyy") + Convert.ToDateTime(dt.Rows[n]["Time"]).ToString("hhmm") ;
                        //val += dt.Rows[n]["Number"].ToString().Substring(2,2);
                        if (Convert.ToInt32(dt.Rows[n]["Number"]) > xmlLastNumber) //Last Number = 5504 xmlLastNumber
                        {
                            amt += Convert.ToDouble(dt.Rows[n]["Amount"]);
                            val += dt.Rows[n]["Number"].ToString().Substring(2, 2);
                            //val += dt.Rows[n]["Number"].ToString() + " : " + dt.Rows[n]["Amount"].ToString() + " :" + amt.ToString();
                            num += "Number = " + dt.Rows[n]["Number"].ToString();
                            m_streamWriter.WriteLine(val);
                            m_streamWriter.WriteLine(num);
                            m_streamWriter.Flush();
                            numOfBill += 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    CreateConfigurationFile(dt, recCount: latestRecCount, number: Convert.ToInt32(dt.Rows[xmlRecordCount]["Number"]), update: true);
                    m_streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), latestRecCount);
                    m_streamWriter.Flush();

                    if ((numOfBill >= 1 || amt >= 50) && xmlInfo[InfoKey.EnableQRCode] == "Enable")
                    {
                        Form2 f2 = new Form2();
                        f2.PrintOut(val, xmlInfo[FileMonitor.InfoKey.Url], num);
                    }

                    //NotifyIcon1.Icon = SystemIcons.Application;
                    //NotifyIcon1.Visible = true;
                    //NotifyIcon1.BalloonTipText = DateTime.Now.ToLongTimeString();
                    //NotifyIcon1.ShowBalloonTip(1000);
                }
            }
        }

        public static DataTable GetDataTable()
        {
            //OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");
            OleDbConnection dbfConnectionHandler = Odbcon();

            if (dbfConnectionHandler.State == ConnectionState.Open)
            {
                OleDbCommand oCmd = dbfConnectionHandler.CreateCommand();
                oCmd.CommandText = @"SELECT * FROM CTP.dbf WHERE NUMBER < 999999";
                DataTable dt = new DataTable();
                dt.Load(oCmd.ExecuteReader());
                dbfConnectionHandler.Close();
                return dt;
            }
            return null;
        }

        //public static string ProcessXML(string xmlText)
        public Dictionary<InfoKey, string> ProcessXML(string xmlText)
        {
            //Dictionary xmlInfo;
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlText);
            XmlNodeList recordCount = doc.GetElementsByTagName("recordcount");
            XmlNodeList number = doc.GetElementsByTagName("number");
            XmlNodeList connection_path = doc.GetElementsByTagName("connection_path");
            XmlNodeList fileName = doc.GetElementsByTagName("filename");
            XmlNodeList url = doc.GetElementsByTagName("url");
            XmlNodeList enableQRCode = doc.GetElementsByTagName("enableQRCode");

            //var xmlInfo = new Dictionary<InfoKey, string>();
            for (int i = 0; i < recordCount.Count; ++i)
            {
                AssignValue(recordCount[i].InnerText,
                number[i].InnerText,
                url[i].InnerText,
                enableQRCode[i].InnerText);
                xmlInfo = new Dictionary<InfoKey, string>()
                {
                    { InfoKey.RecordCount, recordCount[i].InnerText },
                    { InfoKey.Number, number[i].InnerText },
                    { InfoKey.Url, url[i].InnerText },
                    { InfoKey.EnableQRCode, enableQRCode[i].InnerText }
                };
            }
            return xmlInfo;
        }

        public enum InfoKey
        {
            RecordCount = 0,
            Number = 1,
            Url = 2,
            EnableQRCode = 3,
            EnableMinimize = 4,
            Message = 5,
            ConnectionPath = 6,
            FilePath = 7,
            FileName = 8,
            XmlFile = 9
        }

        public static void AssignValue(string recCount, string lastNumber, string url, string enableQRCode)
        {
            xmlRecordCount = Convert.ToInt32(recCount);
            xmlLastNumber = Convert.ToInt32(lastNumber);
            xmlURL = url;
            xmlEnableQRCode = enableQRCode;
        }

        public static void CreateConfigurationFile(DataTable dt, long? recCount = null, int? number = null, string url = "", bool? enableQRCode = true, bool? update = false)
        {
            if (update == false)
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = ("    "),
                    CloseOutput = true,
                    OmitXmlDeclaration = true
                };
                using (XmlWriter writer = XmlWriter.Create(xmlFile, settings))
                {

                    writer.WriteStartElement("root");
                    writer.WriteElementString("recordcount", recCount.ToString());
                    writer.WriteElementString("url", url);

                    if (enableQRCode == true)
                    {
                        writer.WriteElementString("enableQRCode", "Enable");
                    }
                    else
                    {
                        writer.WriteElementString("enableQRCode", "Disable");
                    }

                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        writer.WriteElementString(dt.Columns[i].ToString(), dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString());
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);
                //doc.Load(@"C: \Users\HBTan\source\repos\PsRetailer\WindowsFormsApp1\App.config");

                var idElement = doc.GetElementsByTagName("recordcount");

                if (recCount != null && idElement != null)
                {
                    idElement[0].FirstChild.Value = recCount.ToString();
                }

                if (url != "")
                {
                    idElement = doc.GetElementsByTagName("url");
                    idElement[0].FirstChild.Value = url;
                }

                if (enableQRCode != null)
                {
                    idElement = doc.GetElementsByTagName("enableQRCode");

                    if (enableQRCode == true)
                    {
                        idElement[0].FirstChild.Value = "Enable";
                    }
                    else
                    {
                        idElement[0].FirstChild.Value = "Disable";
                    }
                }

                if (dt != null)
                {
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        idElement = doc.GetElementsByTagName(dt.Columns[i].ToString());

                        if (dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString() != null)
                        {
                            idElement[0].InnerText = dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString();
                        }
                    }
                }
                doc.Save(xmlFile);
            }
        }

        //public static void PrintQRCode(string text1)
        //{
        //    Form2 f2 = new Form2();
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(text1, QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(6);
        //    //f2.pictureBox1.Image = qrCodeImage;
        //    //f2.pictureBox1.Visible = true;
        //    f2.Show();
        //}

        public DataTable CreateDataTable(string s, string s2)
        {
            //OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");
            OleDbConnection dbfConnectionHandler = Odbcon();

            if (dbfConnectionHandler.State == ConnectionState.Open)
            {
                OleDbCommand oCmd = dbfConnectionHandler.CreateCommand();
                oCmd.CommandText = @"INSERT INTO CTP.dbf " + s + " VALUES(" + s2;
                //DataTable dt = new DataTable();
                oCmd.ExecuteReader();
            }
            return null;
        }
    }
}
