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
        FileMonitor pwatcher = new FileMonitor();

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = FileMonitor.GetDataTable();

            int latestRecCount = dt.Rows.Count;
            int latestNumber = Convert.ToInt32(dt.Rows[latestRecCount - 1]["Number"]);

            if (txtXml.Text == "")
            {
                BtnOpenXml_Click(this,null);

                if (!File.Exists(@txtXml.Text))
                {
                    FileMonitor.CreateConfigurationFile(dt, latestRecCount, latestNumber, txtUrl.Text, chkQRCode.Checked, chckMinimize.Checked, txtMsg.Text, txtConnectionPath.Text, txtFilePath.Text, txtFileName.Text, txtXml.Text);
                }
            }

            var xmlInfo = pwatcher.ProcessXML(@txtXml.Text);
 
            txtRecordCount.Text = xmlInfo[FileMonitor.InfoKey.RecordCount];
            mTxtLastNum.Text = xmlInfo[FileMonitor.InfoKey.Number];
            txtUrl.Text = xmlInfo[FileMonitor.InfoKey.Url];
            //xmlInfo[FileMonitor.InfoKey.EnableQRCode] = "Enable" ? chkQRCode.Checked = true : chkQRCode.Checked = false;

            if (xmlInfo[FileMonitor.InfoKey.EnableQRCode] == "Enable")
            {
                chkQRCode.Checked = true;
            }
            else
            {
                chkQRCode.Checked = false;
            }

            if (xmlInfo[FileMonitor.InfoKey.EnableMinimize] == "Enable")
            {
                chckMinimize.Checked = true;
            }
            else
            {
                chckMinimize.Checked = false;
            }

            txtMsg.Text = xmlInfo[FileMonitor.InfoKey.Message];
            txtConnectionPath.Text = xmlInfo[FileMonitor.InfoKey.ConnectionPath];
            txtFilePath.Text = xmlInfo[FileMonitor.InfoKey.FilePath];
            txtFileName.Text = xmlInfo[FileMonitor.InfoKey.FileName];
            txtXml.Text = xmlInfo[FileMonitor.InfoKey.XmlFile];
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to update the configuration?", "Update configuration parameter", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (File.Exists(@txtXml.Text))
                {
                    FileMonitor.CreateConfigurationFile(null, url: txtUrl.Text, enableQRCode: chkQRCode.Checked, enableMinimize: chckMinimize.Checked, message: txtMsg.Text, connectionPath: txtConnectionPath.Text, filePath: txtFilePath.Text, fileName: txtFileName.Text, xmlFileName: txtXml.Text, update: true);
                }
                else
                {
                    FileMonitor.CreateConfigurationFile(dt: null, recCount: Convert.ToInt32(txtRecordCount.Text), number: Convert.ToInt32(mTxtLastNum.Text), url: txtUrl.Text, enableQRCode: chkQRCode.Checked, enableMinimize: chckMinimize.Checked, message: txtMsg.Text, connectionPath: txtConnectionPath.Text, filePath: txtFilePath.Text, fileName: txtFileName.Text, xmlFileName: txtXml.Text);
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
            }
        }
    }
}
