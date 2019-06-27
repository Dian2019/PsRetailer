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
using WindowsFormsApp1.Models;


namespace WindowsFormsApp1
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static FileMonitor Watcher = new FileMonitor();
        //private static readonly string Url = "www.pointsoft.com.my";
        //private static readonly string ConnectionPath = @"D:\PointSoft Dn\Probation Project\20190321\";
        //private static readonly string FileWathcherFilter = "CTP.dbf";
        //private static readonly string FilePath = @"D:\Records.txt";
        private static readonly string XmlFile = @"D:\PsRtrailerConfig.xml";
        //public static string ActXmlFile;
        public static PsRtrailerConfigModel ConfigModel = new PsRtrailerConfigModel();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (!File.Exists(XmlFile))
            //{
            //    Watcher.CreateConfigModelationFile(url: Url, connectionPath: ConnectionPath, fileWatcherFilter: FileWathcherFilter, textFilePath: FilePath, xmlFileName: XmlFile);
            //    //ActXmlFile = XmlFile;
            //}

            if (File.Exists(XmlFile))
            {
                ReadXml();
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show(string.Format("Configuration file {0} not found!", XmlFile));
                Application.Exit();
            }
        }

        public static void ReadXml()
        {
            var xmlInfo = new Dictionary<FileMonitor.XmlKey, string>();
            xmlInfo = Watcher.ProcessXML(XmlFile);
            ConfigModel.Url = xmlInfo[FileMonitor.XmlKey.Url];
            ConfigModel.EnableQRCode = xmlInfo[FileMonitor.XmlKey.EnableQRCode];
            ConfigModel.ConnectionPath = xmlInfo[FileMonitor.XmlKey.ConnectionPath];
            ConfigModel.FileWatcherFilter = xmlInfo[FileMonitor.XmlKey.FileWatcherFilter];
            ConfigModel.FilePath = xmlInfo[FileMonitor.XmlKey.FilePath];
            ConfigModel.XmlFile = xmlInfo[FileMonitor.XmlKey.XmlFile];
            ConfigModel.SmartCode = xmlInfo[FileMonitor.XmlKey.SmartCode];
            ConfigModel.LongDateTime = Convert.ToInt64(xmlInfo[FileMonitor.XmlKey.StateOfDateTime]);
        }
    }
}
