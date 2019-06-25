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
        private static FileMonitor Watcher = new FileMonitor();
        private static readonly string Url = "www.pointsoft.com.my";
        private static readonly string ConnectionPath = @"D:\PointSoft Dn\Probation Project\20190321\";
        private static readonly string FileWathcherFilter = "CTP.dbf";
        private static readonly string FilePath = @"D:\Records.txt";
        private static readonly string XmlFile = @"D:\PsRtrailerConfig.xml";
        public static string ActXmlFile;

        [STAThread]
        static void Main()
        {
            //var xmlInfo = new Dictionary<XmlKey, string>;
            var xmlInfo = new Dictionary<FileMonitor.XmlKey, string>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(XmlFile))
            {
                xmlInfo = Watcher.ProcessXML(XmlFile);
                ActXmlFile = xmlInfo[FileMonitor.XmlKey.XmlFile];
            }
            else
            {
                Watcher.CreateConfigurationFile(url: Url, connectionPath: ConnectionPath, fileWatcherFilter: FileWathcherFilter, textFilePath: FilePath, xmlFileName: XmlFile);
                ActXmlFile = XmlFile;
            }

            //CreateFileWatcher(xmlInfo[FileMonitor.XmlKey.ConnectionPath]);
            //Init();
            Application.Run(new Form1());
        }

        public static Dictionary<FileMonitor.XmlKey, string> XmlInfo1()
        {
            var xmlInfo = new Dictionary<FileMonitor.XmlKey, string>();

            return xmlInfo = Watcher.ProcessXML(ActXmlFile);// !! always same as XmlFile
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

        //public static void CreateFileWatcher(string path)
        //{
        //    FileSystemWatcher watcher = new FileSystemWatcher();
        //    watcher.Path = path;
        //    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
        //       | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        //    watcher.Filter = "CTP.dbf";

        //    watcher.Changed += new FileSystemEventHandler(OnChanged);
        //    watcher.Created += new FileSystemEventHandler(OnChanged);
        //    watcher.Deleted += new FileSystemEventHandler(OnChanged);
        //    watcher.Renamed += new RenamedEventHandler(OnRenamed);

        //    watcher.EnableRaisingEvents = true;
        //}

        //private static void OnChanged(object source, FileSystemEventArgs e)
        //{
        //    FileMonitor fileMon = new FileMonitor();
        //    fileMon.OnFileChange();
        //}

        //private static void OnRenamed(object source, RenamedEventArgs e)
        //{
        //    MessageBox.Show($"File: {e.ChangeType} renamed to {e.FullPath}"); //("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        //}
    }
}
