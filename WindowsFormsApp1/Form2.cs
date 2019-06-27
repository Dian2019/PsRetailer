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
            PrintPreviewDialog1.Document = PrintDocument1;
            PrintPreviewDialog1.Show();
        }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(image, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            e.Graphics.DrawImage(image, 10, 10);
        }

        public Bitmap PrintQRCode(string text1)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text1, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);
            pictureBox1.Image = qrCodeImage;
            pictureBox1.Visible = true;
            return qrCodeImage;
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Size size = TextRenderer.MeasureText(textBox1.Text, textBox1.Font);
            textBox1.Width = size.Width;
            textBox1.Height = size.Height;
            e.Graphics.DrawImage(pictureBox1.Image, (e.PageBounds.Width - pictureBox1.Image.Width)/2,10, pictureBox1.Width, pictureBox1.Height);
            e.Graphics.DrawString(textBox1.Text, textBox1.Font, Brushes.Black, (e.PageBounds.Width- textBox1.Width)/2, pictureBox1.Image.Height + 10);

        }
    }
}
