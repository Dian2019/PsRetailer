using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OleDb;
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
        private static string xmlFile = @"D:\PointSoft Dn\Probation Project\CTP.xml";
        private static FileMonitor Watcher = new FileMonitor();
        private static DataConnection DbfConnection = new DataConnection();

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt = FileMonitor.GetDataTable();

            //var dt = FileMonitor.GetDataTable();

            //int latestRecCount = dt.Rows.Count;
            //int latestNumber = Convert.ToInt32(dt.Rows[latestRecCount - 1]["Number"]);

            //if (txtXml.Text == "")
            //{
            //    BtnOpenXml_Click(this,null);

            //    if (!File.Exists(@txtXml.Text))
            //    {
            //        FileMonitor.CreateConfigurationFile(dt, latestRecCount, latestNumber, txtUrl.Text, chkQRCode.Checked, chckMinimize.Checked, txtMsg.Text, txtConnectionPath.Text, txtFilePath.Text, txtFileName.Text, txtXml.Text);
            //    }
            //}

            //var xmlInfo = Watcher.ProcessXML(@txtXml.Text);
            var xmlInfo = Program.XmlInfo1();

            //txtRecordCount.Text = xmlInfo[FileMonitor.XmlKey.RecordCount];
            //mTxtLastNum.Text = xmlInfo[FileMonitor.XmlKey.Number];
            txtUrl.Text = xmlInfo[FileMonitor.XmlKey.Url];

            if (xmlInfo[FileMonitor.XmlKey.EnableQRCode] == "Enable")
            {
                chkQRCode.Checked = true;
            }
            else
            {
                chkQRCode.Checked = false;
            }

            txtConnectionPath.Text = xmlInfo[FileMonitor.XmlKey.ConnectionPath];
            txtFileWatcherFilter.Text = xmlInfo[FileMonitor.XmlKey.FileWatcherFilter];
            txtFilePath.Text = xmlInfo[FileMonitor.XmlKey.FilePath];
            txtXml.Text = xmlInfo[FileMonitor.XmlKey.XmlFile];

            Watcher.CreateFileWatcher(xmlInfo[FileMonitor.XmlKey.ConnectionPath], xmlInfo[FileMonitor.XmlKey.FileWatcherFilter]);
            //OleDbConnection dbfConnectionHandler = DataConnection.Odbcon(xmlInfo[FileMonitor.XmlKey.ConnectionPath]);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to update the configuration?", "Update configuration parameter", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (File.Exists(@txtXml.Text))
                {
                    Watcher.UpdateConfigurationFile(url: txtUrl.Text, enableQRCode: chkQRCode.Checked, connectionPath: txtConnectionPath.Text, fileWatcherFilter: txtFileWatcherFilter.Text, textFilePath: txtFilePath.Text, xmlFileName: txtXml.Text);
                }
                else
                {
                    Watcher.CreateConfigurationFile(url: txtUrl.Text, enableQRCode: chkQRCode.Checked, connectionPath: txtConnectionPath.Text, fileWatcherFilter: txtFileWatcherFilter.Text, textFilePath: txtFilePath.Text, xmlFileName: txtXml.Text);
                }
            }
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void BtnOpenXml_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oFD = new OpenFileDialog() { Filter = "XML|*.xml", ValidateNames = true })
            {
                if (oFD.ShowDialog() == DialogResult.OK)
                    txtXml.Text = oFD.FileName;
                else
                {
                    
                    this.Dispose(true);
                }
            }
        }

        private void BtnOpenTextFile_Click(object sender, EventArgs e)
        {

        }
    }
}
