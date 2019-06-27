using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var dataConn = new DataConnection();
            DataTable dt = new DataTable();
            FileMonitor fm = new FileMonitor();

            var val1 = "('id','number','group','shopcode','zone','date','time','payby','amount','receive','points','currency','rate','f_receive','tn','pax','staff','ordertype', 'tdate','ttime','cashier','customer','viptype','vip','disctype','dcharge','discount','sc_rate','rx','ry','scharge',";
            val1 += "'cardname','cardnum','cardtype','addrid','cnt','tax','tax1','tax2','tax3','rounding','spflag','encash_id','rider','ref1','ref2','ref3',";
            val1 += "'timer1','timer2','rev','pcount','polled','analysis1','sync','syncdate','syncid','calcost','irewards','irewardrd','ixrewards','ixrewardrd',";
            val1 += "'speccard','oldrv','oldrr','osource','timer2a','bpaamt','nobpaamt','old','bumped','dau1','dau2','location','sba','block','flat','floor','room',";
            val1 += "'num','cca_data','xpoints1','xpoints2','pdpoints','unote','minchg','govinv','govinv2','desc1','desc2','pname1','pname2','mapcode','xxref1',";
            val1 += "'mxtt','mxeo','mxdv','tblkey','sv','qsnum','qstime','cpriv','mpriv','itcount','lasttid','odflag','epdata1','epdata2','traceno','appcode',";
            val1 += "'batchno','crmtype','bstamp1','bstamp2','bstamp3')";
            var val = "'" + txtID.Text + "',";
            val += txtNumber.Text + ",";
            val += "'" + txtGroup.Text + "',";
            val += "'" + textBox4.Text + "',";
            val += "'" + textBox5.Text + "',";
            val += "'" + txtDate.Text + "',";
            val += "'" + txtTime.Text + "',";
            val += txtPayby.Text + ",";
            val += txtAmount.Text + ",";
            val += txtReceive.Text + ",";
            val += txtPoints.Text + ",";
            val += "'" + txtCurr.Text + "',";
            val += txtrate.Text + ",";
            val += txtFR.Text + ",";
            val += "'" + textBox1.Text + "',";
            val += "" + txtPax.Text + ",";
            val += "'" + txtStaff.Text + "',";
            val += "" + txtOrderType.Text + ",";
            val += "'" + txtDate.Text + "',";
            val += "'" + txtTime.Text + "',";
            val += "'" + cashier.Text + "',";
            val += "'" + customer.Text + "',";
            val += "'" + vipType.Text + "','',0,0,0,5.0,'0122L',2,1,'','','',0,0,0,1,2,3,0,1,'1','a','','','',0,0,0,0,'','',1,'0','q',0,0,0,0,0,'',0,0,0,0,0,0,1,0,'','',0,0,";
            val += "'','','','','','',0,0,0,'',0,'','','','','','','','','','','','',0,'000','','','',0,'',0,'','','','','',0,0,0,0)";
            dt = dataConn.CreateDataTable(val1, val);
            fm.OnFileChange();
            this.Close();
        }
    }
}
