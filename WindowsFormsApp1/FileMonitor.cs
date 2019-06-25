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
        private Dictionary<XmlKey, string> xmlInfo;

        public void CreateFileWatcher(string path, string filter)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                /* Watch for changes in LastAccess and LastWrite times, and 
                   the renaming of files or directories. */
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                // Only watch text files.
                Filter = filter
                //Filter = "CTP.dbf"
            };

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            /*watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);*/

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            OnFileChange();
        }

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
            var xmlInfo = ProcessXML(xmlFile);

            var dt = GetDataTable();
            int numOfBill = 0;
            int latestRecCount = dt.Rows.Count;

            if (File.Exists(xmlFile))
            {
                //var xmlInfo = ProcessXML(xmlFile);

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

                    //UpdateConfigurationFile();
                    m_streamWriter.WriteLine("{0} {1}", DateTime.Now.ToString(), latestRecCount);
                    m_streamWriter.Flush();

                    if ((numOfBill >= 1 || amt >= 50) && xmlInfo[XmlKey.EnableQRCode] == "Enable")
                    {
                        Form2 f2 = new Form2();
                        f2.PrintOut(val, xmlInfo[FileMonitor.XmlKey.Url], num);
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

        public Dictionary<XmlKey, string> ProcessXML(string xmlText)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlText);
            //XmlNodeList recordCount = doc.GetElementsByTagName("recordcount");
            //XmlNodeList number = doc.GetElementsByTagName("number");

            XmlNodeList url = doc.GetElementsByTagName("url");
            XmlNodeList enableQRCode = doc.GetElementsByTagName("EnableQRCode");
            XmlNodeList connectionPath = doc.GetElementsByTagName("ConnectionPath");
            XmlNodeList fileWatcherFilter = doc.GetElementsByTagName("FileWatcherFilter");
            XmlNodeList filePath = doc.GetElementsByTagName("FilePath");
            XmlNodeList xmlFile = doc.GetElementsByTagName("XmlFile");

            //var xmlInfo = new Dictionary<XmlKey, string>();
            //for (int i = 0; i < recordCount.Count; ++i)
            {
                /*AssignValue(recordCount[i].InnerText,
                number[i].InnerText,
                url[0].InnerText,
                enableQRCode[0].InnerText);*/
                xmlInfo = new Dictionary<XmlKey, string>()
                {
                    { XmlKey.Url, url[0].InnerText },
                    { XmlKey.EnableQRCode, enableQRCode[0].InnerText },
                    { XmlKey.ConnectionPath, connectionPath[0].InnerText },
                    { XmlKey.FileWatcherFilter, fileWatcherFilter[0].InnerText },
                    { XmlKey.FilePath, filePath[0].InnerText },
                    { XmlKey.XmlFile, xmlFile[0].InnerText }
                };
            }
            return xmlInfo;
        }

        public enum XmlKey
        {
            Url = 0,
            EnableQRCode = 1,
            ConnectionPath = 2,
            FileWatcherFilter = 3,
            FilePath = 4,
            XmlFile = 5
        }

        /*public static void AssignValue(string recCount, string lastNumber, string url, string enableQRCode)
        {
            xmlRecordCount = Convert.ToInt32(recCount);
            xmlLastNumber = Convert.ToInt32(lastNumber);
            xmlURL = url;
            xmlEnableQRCode = enableQRCode;
        }*/

        //public static void CreateConfigurationFile(DataTable dt, long? recCount = null, int? number = null, string url = "", bool? enableQRCode = true, bool? enableMinimize = true, string message = "", string connectionPath = "", string filePath = "", string fileName = "", string xmlFileName = "", bool? update = false)
        public void CreateConfigurationFile(string url = "", bool? enableQRCode = true, string connectionPath = "", string fileWatcherFilter = "", string textFilePath = "", string xmlFileName = "")
        {
            string enable;

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = ("    "),
                CloseOutput = true,
                OmitXmlDeclaration = true
            };
            using (XmlWriter writer = XmlWriter.Create(@xmlFileName, settings))
            {
                writer.WriteStartElement("root");
                writer.WriteElementString("url", url);

                enable = (enableQRCode == true) ? "Enable" : "Disable";
                writer.WriteElementString("EnableQRCode", enable);

                writer.WriteElementString("ConnectionPath", connectionPath);
                writer.WriteElementString("FileWatcherFilter", fileWatcherFilter);
                writer.WriteElementString("FilePath", textFilePath);
                writer.WriteElementString("XmlFile", xmlFileName);

                /*if (dt != null)
                { 
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        writer.WriteElementString(dt.Columns[i].ToString(), dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString());
                    }
                }*/
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        public void UpdateConfigurationFile(string url = "", bool? enableQRCode = true, string connectionPath = "", string fileWatcherFilter = "",  string textFilePath = "", string xmlFileName = "")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);

            //doc.Load(@"C: \Users\HBTan\source\repos\PsRetailer\WindowsFormsApp1\App.config");

            //var idElement = doc.GetElementsByTagName("recordcount");

            //if (recCount != null && idElement != null) doc.GetElementsByTagName("recordcount")[0].FirstChild.Value = recCount.ToString();

            if (url != "") doc.GetElementsByTagName("url")[0].FirstChild.Value = url;

            if (enableQRCode != null) doc.GetElementsByTagName("enableQRCode")[0].FirstChild.Value = (enableQRCode == true) ? "Enable" : "Disable";

            //if (enableMinimize != null) doc.GetElementsByTagName("enableMinimize")[0].FirstChild.Value = (enableMinimize == true) ? "Enable" : "Disable";

            //if (message != "") doc.GetElementsByTagName("Message")[0].FirstChild.Value = message;

            if (connectionPath != "") doc.GetElementsByTagName("ConnectionPath")[0].FirstChild.Value = connectionPath;

            if (textFilePath != "") doc.GetElementsByTagName("FilePath")[0].FirstChild.Value = textFilePath;

            if (xmlFileName != "") doc.GetElementsByTagName("XmlFile")[0].FirstChild.Value = xmlFileName;

            /*if (dt != null)
            {
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    idElement = doc.GetElementsByTagName(dt.Columns[i].ToString());

                    if (dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString() != null)
                    {
                        idElement[0].InnerText = dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString();
                    }
                }
            }*/
            doc.Save(xmlFile);
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
