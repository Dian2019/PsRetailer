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

namespace WindowsFormsApp1
{
    public class FileMonitor
    {
        string val;
        long dateTimeState, dateTimeStateVal;
        static double amt;
        string num;
        private Dictionary<XmlKey, string> xmlInfo;

        public void CreateFileWatcher(string @path, string filter)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                // Only watch text files.
                Filter = filter

            };

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //OnFileChange();
        }

        public void OnFileChange()
        {
            //Program.ReadXml();
            FileStream fs = new FileStream(@Program.ConfigModel.FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            //private static readonly FileStream fs = new FileStream(@"D:\PointSoft Dn\Probation Project\mcb.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);

            var dt = new DataTable();
            dt = DataConnection.GetDataTable(Program.ConfigModel.ConnectionPath);
            int numOfBill = 0;
            int latestRecCount = dt.Rows.Count;

            if (File.Exists(Program.ConfigModel.XmlFile))
            {             
                for (var n = dt.Rows.Count-1; n >= 1; n--)
                {
                    //dateTimeState = Convert.ToInt64(Convert.ToDateTime(dt.Rows[n]["Date"]).ToString("yyyyMMdd") + Convert.ToDateTime(dt.Rows[n]["Time"]).ToString("hhmm"));

                    dateTimeState = Convert.ToInt64(Convert.ToDateTime(dt.Rows[n]["Date"]).ToString("yyyyMMdd")
                        + Convert.ToDateTime(dt.Rows[n]["Time"]).ToString("hhmm")
                        + dt.Rows[n]["Number"].ToString().Substring(2, 2));
                    if (dateTimeState > Program.ConfigModel.LongDateTime) //Last Number = 5504 xmlLastNumber
                    {
                        amt += Convert.ToDouble(dt.Rows[n]["Amount"]);
                        val = dt.Rows[n]["Shopcode"].ToString().Trim();
                        val += Convert.ToDateTime(dt.Rows[n]["Date"]).ToString("ddMMyyyy") + Convert.ToDateTime(dt.Rows[n]["Time"]).ToString("hhmm");
                        val += dt.Rows[n]["Number"].ToString().Substring(2, 2);
                        num += "Number = " + dt.Rows[n]["Number"].ToString() + " ";
                        m_streamWriter.WriteLine(val);
                        m_streamWriter.WriteLine(num);
                        m_streamWriter.Flush();
                        numOfBill += 1;
                        dateTimeStateVal = dateTimeState;
                    }
                    else
                    {
                        break;
                    }
                }

                UpdateConfigurationFile(smartCode: val, timeState: dateTimeStateVal);
                m_streamWriter.WriteLine("{0} LatestCount: {1}", DateTime.Now.ToString(), latestRecCount);
                m_streamWriter.Flush();

                if ((numOfBill >= 1 || amt >= 50) && Program.ConfigModel.EnableQRCode == "Enable")
                {
                    Form2 f2 = new Form2();
                    f2.PrintOut(val, Program.ConfigModel.Url, num);
                }
                dt.Dispose();
            }
            m_streamWriter.Close();
        }

        public Dictionary<XmlKey, string> ProcessXML(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlText);

            XmlNodeList url = doc.GetElementsByTagName("url");
            XmlNodeList enableQRCode = doc.GetElementsByTagName("EnableQRCode");
            XmlNodeList connectionPath = doc.GetElementsByTagName("ConnectionPath");
            XmlNodeList fileWatcherFilter = doc.GetElementsByTagName("FileWatcherFilter");
            XmlNodeList filePath = doc.GetElementsByTagName("FilePath");
            XmlNodeList xmlFile = doc.GetElementsByTagName("XmlFile");
            XmlNodeList smartCode = doc.GetElementsByTagName("SmartCode");
            XmlNodeList stateOfDateTime = doc.GetElementsByTagName("SateOfDateTime");

            xmlInfo = new Dictionary<XmlKey, string>()
            {
                { XmlKey.Url, url[0].InnerText },
                { XmlKey.EnableQRCode, enableQRCode[0].InnerText },
                { XmlKey.ConnectionPath, connectionPath[0].InnerText },
                { XmlKey.FileWatcherFilter, fileWatcherFilter[0].InnerText },
                { XmlKey.FilePath, filePath[0].InnerText },
                { XmlKey.XmlFile, xmlFile[0].InnerText },
                { XmlKey.SmartCode, smartCode[0].InnerText },
                { XmlKey.StateOfDateTime, stateOfDateTime[0].InnerText }
            };
            
            return xmlInfo;
        }

        public enum XmlKey
        {
            Url = 0,
            EnableQRCode = 1,
            ConnectionPath = 2,
            FileWatcherFilter = 3,
            FilePath = 4,
            XmlFile = 5,
            SmartCode = 6,
            StateOfDateTime = 7
        }

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
                writer.WriteStartElement("configuration");
                writer.WriteElementString("url", url);

                enable = (enableQRCode == true) ? "Enable" : "Disable";
                writer.WriteElementString("EnableQRCode", enable);

                writer.WriteElementString("ConnectionPath", connectionPath);
                writer.WriteElementString("FileWatcherFilter", fileWatcherFilter);
                writer.WriteElementString("FilePath", textFilePath);
                writer.WriteElementString("XmlFile", xmlFileName);
                writer.WriteElementString("SmartCode", "0");
                writer.WriteElementString("SateOfDateTime", "0");
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        public void UpdateConfigurationFile(string url = "", bool? enableQRCode = true, string connectionPath = "", string fileWatcherFilter = "",  string textFilePath = "", string xmlFileName = "", string smartCode = "", long? timeState = null)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Program.ConfigModel.XmlFile);

            if (url != "") doc.GetElementsByTagName("url")[0].FirstChild.Value = url;
            if (enableQRCode != null) doc.GetElementsByTagName("EnableQRCode")[0].FirstChild.Value = (enableQRCode == true) ? "Enable" : "Disable";
            if (connectionPath != "") doc.GetElementsByTagName("ConnectionPath")[0].FirstChild.Value = connectionPath;
            if (textFilePath != "") doc.GetElementsByTagName("FilePath")[0].FirstChild.Value = textFilePath;
            if (xmlFileName != "") doc.GetElementsByTagName("XmlFile")[0].FirstChild.Value = xmlFileName;
            if (smartCode != "") doc.GetElementsByTagName("SmartCode")[0].FirstChild.Value = smartCode;
            if (timeState != null) doc.GetElementsByTagName("SateOfDateTime")[0].FirstChild.Value = timeState.ToString();

            doc.Save(Program.ConfigModel.XmlFile);
        }
    }
}
