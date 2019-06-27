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

namespace WindowsFormsApp1
{
    public class DataConnection
    {
        private static OleDbConnection DbfConnection(string path)
        {
            OleDbConnection oleConnectHandle = new OleDbConnection(
                @"Provider=VFPOLEDB.1;Data Source=" + path);
            oleConnectHandle.Open();

            return oleConnectHandle;
        }

        public static OleDbConnection Odbcon(string path)
        {
            OleDbConnection dbfConnectionHandler = DbfConnection(@path);

            return dbfConnectionHandler;
        }

        public static DataTable GetDataTable(string path)
        {
            OleDbConnection dbfConnectionHandler = Odbcon(Program.ConfigModel.ConnectionPath);

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

        public DataTable CreateDataTable(string s, string s2)
        {
            OleDbConnection dbfConnectionHandler = Odbcon(Program.ConfigModel.ConnectionPath);

            if (dbfConnectionHandler.State == ConnectionState.Open)
            {
                OleDbCommand oCmd = dbfConnectionHandler.CreateCommand();
                oCmd.CommandText = @"INSERT INTO CTP.dbf " + s + " VALUES(" + s2;
                oCmd.ExecuteReader();
            }
            return null;
        }
    }
}