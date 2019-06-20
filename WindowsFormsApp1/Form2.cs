using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        Bitmap image;
        public Form2()
        {
            InitializeComponent();
        }

        public void PrintOut(string qrStr, string url, string msg)
        {
            pictureBox1.Image = PrintQRCode(qrStr);
            textBox1.Text = url;
            //textBox2.Text = msg;
            //printDocument1.PrintPage += PrintPage;
            //printDialog1.Document = printDocument1;
            //PrintPreviewDialog1.Document = printDialog1.Document;
            PrintPreviewDialog1.Document = printDocument1;
            PrintPreviewDialog1.Show();
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(image, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            e.Graphics.DrawImage(image, 10, 10);
        }

        private void button1_Click2(object sender, EventArgs e)
        {
            PrintQRCode("1234");
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Rectangle rectangle = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            //this.panel1.DrawToBitmap(image, rectangle);

            PrintPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
            //PrintPreviewDialog1.Show();
            //printPreviewControl1.Document = printDocument1;
        }

        public Bitmap PrintQRCode(string text1)
        {
            //Form2 f2 = new Form2();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text1, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);
            pictureBox1.Image = qrCodeImage;
            pictureBox1.Visible = true;
            return qrCodeImage;
            //f2.Show();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Rectangle pageArea = e.PageBounds;
            //e.Graphics.DrawImage(pictureBox1.Image, 10, 10, pictureBox1.Width, pictureBox1.Height);
            e.Graphics.DrawImage(pictureBox1.Image, (e.PageBounds.Width - pictureBox1.Image.Width)/2,10, pictureBox1.Width, pictureBox1.Height);
            e.Graphics.DrawString(textBox1.Text, textBox1.Font, Brushes.Black, (e.PageBounds.Width- textBox1.Width)/2, pictureBox1.Image.Height + 10);
            //e.Graphics.DrawString(textBox2.Text, textBox2.Font, Brushes.Black, 200, 800);
            //e.Graphics.DrawImage(pictureBox2.Image, 50, 500, pictureBox2.Width, pictureBox2.Height);
        }

        public void GetPrintArea(Panel pnl)
        {
            image = new Bitmap(pnl.Width, pnl.Height);
            //image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            //Rectangle rect = new Rectangle(0, 0, pictureBox2.Width, pictureBox2.Height);
            //pnl.DrawToBitmap(image, rect);


            //image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //Rectangle rectangle = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            //this.panel1.DrawToBitmap(image, rectangle);
            
        }

        public void Print()
        {
            //GetPrintArea(pnl);
            PrintPreviewDialog1.Document = printDocument1;
            PrintPreviewDialog1.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //print label
            //GetPrintArea(panel1);
            printDocument1.Print();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = PrintQRCode("1234");
            //print preview
            Print();
        }
    }
}
