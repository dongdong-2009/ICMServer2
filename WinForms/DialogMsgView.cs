using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogMsgView : Form
    {
        int m_MsgId;
        public DialogMsgView(int msgId)
        {
            InitializeComponent();
            m_MsgId = msgId;
        }

        private void MessageView_Load(object sender, EventArgs e)
        {
            using (var db = new ICMServer.Models.ICMDBContext())
            {
                /**
                 * Query modify item
                 **/
                var query = from zz in db.Publishinfoes
                            where zz.id == m_MsgId
                            select zz;
                foreach (var Msg in query)
                {
                    textBox.Visible = false;
                    pictureBox.Visible = true;
                    textBoxTitle.Text = Msg.title;
                    textBoxTime.Text = Msg.time.ToString();
                    if (Msg.type == 1)
                        textBoxType.Text = "Image";
                    else
                        textBoxType.Text = "Text";
                    textBoxAddress.Text = DevicesAddressConverter.RoToChStr(Msg.dstaddr);

                    string picPath = Path.GetAppExeFolderPath() + @"\" + Msg.filepath;
                    pictureBox.Image = new Bitmap(picPath);
                }
            }
        }
    }
}
