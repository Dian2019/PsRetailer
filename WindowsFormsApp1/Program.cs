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

        public static void CreateFileWatcher(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "CTP.dbf";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            FileMonitor fileMon = new FileMonitor();
            fileMon.OnFileChange();
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            MessageBox.Show($"File: {e.ChangeType} renamed to {e.FullPath}"); //("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}
