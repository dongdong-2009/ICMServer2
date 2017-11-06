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
using System.Xml.Linq;

namespace ICMServer
{
    public partial class DialogCardImport : Form
    {
        List<Iccard> m_Cards = new List<Iccard>();

        public DialogCardImport()
        {
            InitializeComponent();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgFile = new OpenFileDialog();
            dlgFile.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            dlgFile.FileName = "cardlist.xml";
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                XDocument doc = XDocument.Load(dlgFile.FileName);
                var DeviceNode = doc.Root.Element("dev");
                int DeviceType = int.Parse(DeviceNode.Attribute("ty").Value);
                string DeviceId = DeviceNode.Attribute("ro").Value;
                m_Cards = (from card in DeviceNode.Elements("card")
                           select new Iccard { C_icno = card.Value }).ToList();
                iccardBindingSource.DataSource = m_Cards.ToList();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var card = iccardBindingSource.Current as Iccard;
            if (card == null)
                return;

            //DialogAddCard dlg = new DialogAddCard(card.C_icno);
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{

            //}
        }
    }
}
