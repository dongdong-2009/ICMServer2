using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogMsgCreate : Form
    {
        publishinfo m_PublishInfo = null;

        public DialogMsgCreate()
        {
            InitializeComponent();
        }

        public publishinfo GetReturnValue()
        {
            return m_PublishInfo;
        }

        // 发布
        private void BtnRelease_Click(object sender, EventArgs e)
        {
            if (txtBoxTitle.Text == "" || txtBoxAddress.Text == "")
            {
                MessageBox.Show(strings.SendAddrOrMsgCannotBeNull);//"发送地址或信息标题不得为空!");
                return;
            }
            if (txtBoxTitle.Text.Length > 50)
            {
                MessageBox.Show("标题長度不可以大于50個字元!");
                return;
            }
            if (radioBtnText.Checked && textBox.Text.TrimEnd() == "")
            {
                MessageBox.Show("內容不得为空!");
                return;
            }
            if (radioBtnPic.Checked && txtBoxPicSource.Text == "")
            {
                MessageBox.Show("发送图片地址不能为空!");
                return;
            }

            m_PublishInfo = new publishinfo
            {
                type = radioBtnText.Checked ? 0 : 1,  // 0: text, 1: picture
                dstaddr = txtBoxAddress.Text,
                title = txtBoxTitle.Text,
                time = DateTime.Now,
                isread = 0//false;
            };
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            // [TODO] bmp 室内机可能无法顯示 =_=
            string filename = string.Format(timeStamp + ".jpg");
            string destFile = Path.GetPublishInfoFolderPath() + @"\" + filename;
            m_PublishInfo.filepath = Path.GetPublishInfoFolderRelativePath() + @"\" + filename;

            if (radioBtnPic.Checked == true)
            {
                File.Copy(txtBoxPicSource.Text, destFile, true);
            }
            else
            {
                TextJpgConvert(textBox.Text, textBox.Font.ToString(), textBox.Font.Size, destFile);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void TextJpgConvert(string txt, string fontname, float fontsize, string filepath)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(bmp);
            Font font = new Font(fontname, fontsize);
            SizeF stringSize = graphics.MeasureString(txt, font);
            int width = Math.Max(this.textBox.Width, (int)stringSize.Width);
            int height = Math.Max(this.textBox.Height, (int)stringSize.Height);

            width = (width + 7) / 8 * 8;
            height = (height + 7) / 8 * 8;

            bmp = new Bitmap(bmp, width, height);
            graphics = Graphics.FromImage(bmp);
            RectangleF rect = new RectangleF(0, 0, width, height);
            graphics.FillRectangle(new SolidBrush(Color.White), rect);
            graphics.DrawString(txt, font, Brushes.Black, rect);
            graphics.Flush();

            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp.Save(filepath, jgpEncoder, myEncoderParameters); 
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        // 开閉
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 初始化
        private void CreateMsg_Load(object sender, EventArgs e)
        {
            radioBtnText.Checked = true;    // 预设勾选 text
        }

        private void radioButtonPic_CheckedChanged(object sender, EventArgs e)
        {
            picBox.Visible = radioBtnPic.Checked;           // 如果 picture radio button 是 checked
            BtnSetFont.Visible = !radioBtnPic.Checked;      // 如果 picture radio button 是 unchecked
            textBox.Visible = !radioBtnPic.Checked;         // 如果 picture radio button 是 unchecked
            labelPic.Visible = radioBtnPic.Checked;         // 如果 picture radio button 是 checked
            txtBoxPicSource.Visible = radioBtnPic.Checked;  // 如果 picture radio button 是 checked
            BtnPicImport.Visible = radioBtnPic.Checked;     // 如果 picture radio button 是 checked
        }

        // 點擊选字型按鈕
        private void BtnSetFont_Click(object sender, EventArgs e)
        {
            FontDialog fontset = new FontDialog();
            if (fontset.ShowDialog() == DialogResult.OK)
                textBox.Font = fontset.Font;
        }

        private void BtnPicImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = strings.OpenOldFile,// "开启存档";
                Filter = "JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // TODO: 如果導入的不是图片怎么办？（错误处理）
                picBox.Image = new Bitmap(dlg.FileName);
                txtBoxPicSource.Text = dlg.FileName;
            }
        }

        private void BtnAddressList_Click(object sender, EventArgs e)
        {
            DialogAreaAddress dlg = new DialogAreaAddress();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBoxAddress.Text = dlg.ReturnValue;
            }
        }
    }
}
