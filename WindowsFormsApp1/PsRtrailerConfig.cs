using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class PsRtrailerConfigModel
    {
        public string Url { get; set; }
        public string EnableQRCode { get; set; }
        public string ConnectionPath { get; set; }
        public string FileWatcherFilter { get; set; }
        public string FilePath { get; set; }
        public string XmlFile { get; set; }
        public string SmartCode { get; set; }
        public long LongDateTime { get; set; }
    }
}