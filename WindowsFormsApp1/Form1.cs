using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency;
using TableDependency.SqlClient;
using DotNetDBF;
using DotNetDBF.Enumerable;
using System.Xml;
using System.Security.Permissions;
using System.Configuration;
using System.Collections.Specialized;
using System.Linq;
using QRCoder;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        OleDbConnection yourConnectionHandle;
        static string val;
        static double amt;
        static int xmlRecordCount;
        static int xmlLastNumber;

        private static string xmlFile = @"D:\PointSoft Dn\Probation Project\CTP.xml";
        private static FileStream fs = new FileStream(@"D:\PointSoft Dn\Probation Project\mcb.txt", FileMode.OpenOrCreate, FileAccess.Write);
        private static StreamWriter m_streamWriter = new StreamWriter(fs);
        
        

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            dt = GetDataTable();

            int latestRecCount = dt.Rows.Count;
            int latestNumber = Convert.ToInt32(dt.Rows[0]["Number"]);

            //dataGridView1.DataSource = dt;

            if (!File.Exists(xmlFile))
            {
                CreateConfigurationFile(dt, latestRecCount, latestNumber);
            }
            else
            {
                ProcessXML(xmlFile);
            }
            CreateFileWatcher(@"D:\PointSoft Dn\Probation Project\20190321\");
        }


        public void CreateFileWatcher(string path)
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

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            OnFileChange();
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            MessageBox.Show($"File: {e.ChangeType} renamed to {e.FullPath}"); //("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }


        private OleDbConnection DbfConnection(string path)
        {
            OleDbConnection oleConnectHandle = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + path);
            oleConnectHandle.Open();

            return oleConnectHandle;
        }

        private DataTable GetDataTable()
        {
            OleDbConnection dbfConnectionHandler = DbfConnection(@"D:\PointSoft Dn\Probation Project\20190321\");

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


        private void OnFileChange()
        {
            DataTable dt = new DataTable();
            dt = GetDataTable();
            int numOfBill = 0;

            int latestRecCount = dt.Rows.Count;

            if (File.Exists(xmlFile))
            {
                ProcessXML(xmlFile);

                if (latestRecCount > xmlRecordCount)
                {

                    for (var n = xmlRecordCount; n < dt.Rows.Count; n++)
                    {
                        val = dt.Rows[n]["Number"].ToString() + " : " + dt.Rows[n]["Shopcode"].ToString()+" "+ dt.Rows[n]["Date"].ToString();
                        val += " " + dt.Rows[n]["Time"].ToString();

                        if (Convert.ToInt32(dt.Rows[n]["Number"]) > xmlLastNumber) //Last Number = 5504 xmlLastNumber
                        {
                            amt += Convert.ToDouble(dt.Rows[n]["Amount"]);
                            val += " : " + dt.Rows[n]["Amount"].ToString() + " :" + amt.ToString();
                            m_streamWriter.WriteLine(val);
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

                    if (numOfBill >= 1 || amt >= 50)
                    {
                        PrintQRCode(val);
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                    }

                    NotifyIcon1.Icon = SystemIcons.Application;
                    NotifyIcon1.Visible = true;
                    NotifyIcon1.BalloonTipText = DateTime.Now.ToLongTimeString();
                    NotifyIcon1.ShowBalloonTip(1000);
                }
            }
        }

 
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                NotifyIcon1.Visible = true;
                NotifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                NotifyIcon1.Visible = false;
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                NotifyIcon1.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool mousePointerOffTaskbar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized)
            {
                NotifyIcon1.Icon = SystemIcons.Application;
                NotifyIcon1.Visible = true;
                NotifyIcon1.BalloonTipText = "The program is minimized to system tray";
                NotifyIcon1.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
            }
        }

        public void CreateConfigurationFile(DataTable dt, long? recCount = null,  int? number = null, bool? update = false)
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
                    for (var i = 0; i < dt.Columns.Count; i++)
                    {
                        writer.WriteElementString(dt.Columns[i].ToString(), dt.Rows[0][i].ToString());
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

                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    idElement = doc.GetElementsByTagName(dt.Columns[i].ToString());

                    if (dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString() != null)
                    {
                        idElement[0].InnerText = dt.Rows[Convert.ToInt32(recCount) - 1][i].ToString();
                    }
                }
                doc.Save(xmlFile);
            }
        }


        public void AssignValue(String recCount, String lastNumber)
        {
            xmlRecordCount = Convert.ToInt32(recCount);
            xmlLastNumber = Convert.ToInt32(lastNumber);
        }

        public void ProcessXML(String xmlText)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            XmlDocument _doc = new XmlDocument();
            _doc.Load(xmlText);
            XmlNodeList _recordcount = _doc.GetElementsByTagName("recordcount");
            XmlNodeList number = _doc.GetElementsByTagName("number");
            XmlNodeList _connection_path = _doc.GetElementsByTagName("connection_path");
            XmlNodeList _filename = _doc.GetElementsByTagName("filename");
            XmlNodeList _timerinterval = _doc.GetElementsByTagName("timerinterval");

            for (int _i = 0; _i < _recordcount.Count; ++_i)
            {
                AssignValue(_recordcount[_i].InnerText,
                number[_i].InnerText);
            }
        }

        public void PrintQRCode(string text1)
        {
            Form2 f2 = new Form2();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(6);
            f2.pictureBox1.Image = qrCodeImage;
            f2.pictureBox1.Visible = true;
            f2.Show();
        }

        public static void GetConfigurationUsingSection()
        {
            var applicationSettings = ConfigurationManager.GetSection("ApplicationSettings") as NameValueCollection;
            if (applicationSettings.Count == 0)
            {
                Console.WriteLine("Application Settings are not defined");
            }
            else
            {
                foreach (var key in applicationSettings.AllKeys)
                {
                    Console.WriteLine(key + " = " + applicationSettings[key]);
                }
            }
        }
    }
}
