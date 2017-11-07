using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace ICMServer
{
    public partial class DialogShowQR : Form
    {
        string qrContent = "";
        public DialogShowQR(string qrDetail)
        {
            InitializeComponent();
            qrContent = qrDetail;
        }

        private void DialogShowQR_Load(object sender, EventArgs e)
        {
            BarcodeWriter bw = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            bw.Options.Width = 260;
            bw.Options.Height = 237;
            Bitmap bitmap = bw.Write(qrContent);
            pictureBoxQR.Image = (Image)bitmap;
        }
    }
}
