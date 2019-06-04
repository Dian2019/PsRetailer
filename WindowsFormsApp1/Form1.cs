using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        private bool isClosed = true;
        private BinaryReader _dataInputStream;
        private DBFHeader _header;
        private string _dataMemoLoc;

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
            int latestNumber = Convert.ToInt16(dt.Rows[0]["Number"]);

            dataGridView1.DataSource = dt;

            if (!File.Exists(xmlFile))
            {
                CreateConfigurationFile(latestRecCount, latestNumber);
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
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            MessageBox.Show($"File: {e.FullPath} {e.ChangeType}");
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
                oCmd.CommandText = @"SELECT * FROM CTP.dbf WHERE number<999999 order by number DESC";
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

            int latestRecCount = dt.Rows.Count;

            if (File.Exists(xmlFile))
            {
                ProcessXML(xmlFile);

                if (latestRecCount > xmlRecordCount)
                {
                    for (var n = 0; n < dt.Rows.Count; n++)
                    {
                        val = dt.Rows[n]["Number"].ToString();

                        if (Convert.ToInt16(dt.Rows[n]["Number"]) > xmlLastNumber) //Last Number = 5504 xmlLastNumber
                        {
                            amt += Convert.ToDouble(dt.Rows[n]["Amount"]);
                            val += " :" + dt.Rows[n]["Amount"].ToString() + " :" + amt.ToString();
                            m_streamWriter.WriteLine(val);
                            m_streamWriter.Flush();
                        }
                        else
                        {
                            break;
                        }
                    }
                    CreateConfigurationFile(_number: Convert.ToInt16(dt.Rows[0]["Number"]), update: true);


                    m_streamWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), latestRecCount);
                    m_streamWriter.Flush();
                    NotifyIcon1.Icon = SystemIcons.Application;
                    NotifyIcon1.Visible = true;
                    NotifyIcon1.BalloonTipText = DateTime.Now.ToLongTimeString();
                    NotifyIcon1.ShowBalloonTip(1000);

                    CreateConfigurationFile(_recCount: latestRecCount, update: true);
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

        public void CreateConfigurationFile(long? _recCount = null, int? _number = null, bool? update = false)
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
                    writer.WriteElementString("recordcount", _recCount.ToString());
                    writer.WriteElementString("number", _number.ToString());
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);

                var idElement = doc.GetElementsByTagName("recordcount");

                if (_recCount != null && idElement != null)
                {
                    idElement[0].FirstChild.Value = _recCount.ToString();
                }

                idElement = doc.GetElementsByTagName("number");

                if (_number != null && idElement != null)
                {
                    idElement[0].FirstChild.Value = _number.ToString();
                }

                doc.Save(xmlFile);
            }
        }


        public void AssignValue(String recCount, String lastNumber)
        {
            xmlRecordCount = Convert.ToInt16(recCount);
            xmlLastNumber = Convert.ToInt16(lastNumber);
        }

        public void ProcessXML(String xmlText)
        {
            XmlDocument _doc = new XmlDocument();
            _doc.Load(xmlText);  
            XmlNodeList _recordcount = _doc.GetElementsByTagName("recordcount");
            XmlNodeList _number = _doc.GetElementsByTagName("number");
            XmlNodeList _connection_path = _doc.GetElementsByTagName("connection_path");
            XmlNodeList _filename = _doc.GetElementsByTagName("filename");
            XmlNodeList _timerinterval = _doc.GetElementsByTagName("timerinterval");

            for (int _i = 0; _i < _recordcount.Count; ++_i)
            {
                AssignValue(_recordcount[_i].InnerText,
                _number[_i].InnerText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TestSelectPath = @"D:\PointSoft Dn\Probation Project\20190321\DBT.DBF";


            //DBFReader2 rd = new DBFReader2();
            //using (Stream fis = File.Open(TestSelectPath,
            //                            FileMode.OpenOrCreate,
            //                            FileAccess.ReadWrite))
            //{
            //    using (var reader = new DBFReader(TestSelectPath)
            //    {
            //        DataMemoLoc = Path.ChangeExtension(TestSelectPath, "DBT")
            //    };
            //    var readValues = reader.DynamicAllRecords();
            //};


            using (
                Stream fis =
                    File.Open(TestSelectPath,
                              FileMode.OpenOrCreate,
                              FileAccess.ReadWrite))
            {
                var reader = new DBFReader(fis)
                {
                    DataMemoLoc = Path.ChangeExtension(TestSelectPath, "DBT")
                };

                //var readValues = reader.AllRecords(new { F2 = default(decimal), F3 = default(string) });
                var readValues = reader.DynamicAllRecords();
                //Assert.That(readValues.First().F3, StartsWith(writtenValue), "Written Value not equaling Read");
            }



        }




        public DBFReader(Stream anIn)
        {
            try
            {
                _dataInputStream = new BinaryReader(anIn);
                isClosed = false;
                _header = new DBFHeader();
                _header.Read(_dataInputStream);

                /* it might be required to leap to the start of records at times */
                int t_dataStartIndex = _header.HeaderLength
                                       - (32 + (32 * _header.FieldArray.Length))
                                       - 1;
                if (t_dataStartIndex > 0)
                {
                    _dataInputStream.ReadBytes((t_dataStartIndex));
                }
            }
            catch (IOException e)
            {
                throw new DBFException("Failed To Read DBF", e);
            }
        }

        /**
   Returns the number of records in the DBF.
   */


        public int RecordCount
        {
            get { return _header.NumberOfRecords; }
        }

        /**
   Returns the asked Field. In case of an invalid index,
   it returns a ArrayIndexOutofboundsException.

   @param index. Index of the field. Index of the first field is zero.
   */

        public DBFField[] Fields
        {
            get { return _header.FieldArray; }
        }

        #region IDisposable Members

        /// <summary>Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.</summary>
        /// <filterpriority>2</filterpriority>

        #endregion


        public string DataMemoLoc
        {
            get
            {
                return _dataMemoLoc;
            }
            set
            {
                _dataMemoLoc = value;
            }
        }

        public override String ToString()
        {
            StringBuilder sb =
                new StringBuilder(_header.Year + "/" + _header.Month + "/"
                                  + _header.Day + "\n"
                                  + "Total records: " + _header.NumberOfRecords +
                                  "\nHeader length: " + _header.HeaderLength +
                                  "");

            for (int i = 0; i < _header.FieldArray.Length; i++)
            {
                sb.Append(_header.FieldArray[i].Name);
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public Object[] NextRecord()
        {
            return NextRecord(_selectFields, _orderedSelectFields);
        }
        public DBFReader(string anIn)
        {
            try
            {
                var fileStream = File.Open(anIn,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.ReadWrite);
                var buff = new byte[2 * 1024 * 1024];
                var count = fileStream.Read(buff, 0, buff.Length);
                var memStream = new MemoryStream(buff, 0, count);
                _dataInputStream = new BinaryReader(memStream);
                //var dbtPath = Path.ChangeExtension(anIn, "dbt");
                //if (File.Exists(dbtPath))
                //{
                //    _dataMemoLoc = dbtPath;
                //}

                isClosed = false;
                _header = new DBFHeader();
                _header.Read(_dataInputStream);

                /* it might be required to leap to the start of records at times */
                int t_dataStartIndex = _header.HeaderLength
                                       - (32 + (32 * _header.FieldArray.Length))
                                       - 1;
                if (t_dataStartIndex > 0)
                {
                    _dataInputStream.ReadBytes((t_dataStartIndex));
                }
            }
            catch (IOException ex)
            {
                throw new DBFException("Failed To Read DBF", ex);
            }
        }
    }
}
