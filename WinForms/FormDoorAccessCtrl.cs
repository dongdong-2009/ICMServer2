using Equin.ApplicationFramework;
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
using System.Xml;
using System.Xml.Linq;

namespace ICMServer
{
    public partial class FormDoorAccessCtrl : Form
    {
        ICMDBContext db = new ICMDBContext();

        public FormDoorAccessCtrl()
        {
            InitializeComponent();
        }

        private void FormDoorAccessCtrl_Load(object sender, EventArgs e)
        {
            this.iccardBindingSource.DataSource = new BindingListView<Iccard>(db.Iccards.ToList());
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshCardList();
        }

        void RefreshCardList()
        {
            this.iccardBindingSource.DataSource = new BindingListView<Iccard>(db.Iccards.ToList());
            string roomId = textBoxQueryRo.Text;
            string cardNumber = textBoxQueryCard.Text;
            string userName = textBoxQueryName.Text;
            BindingListView<Iccard> cards = (BindingListView<Iccard>)iccardBindingSource.DataSource;

            if (roomId == "" && cardNumber == "" && userName == "")
            {
                cards.Filter = null;
            }
            else if (roomId != "" && cardNumber == "" && userName == "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => c.C_roomid == roomId);
            }
            else if (roomId == "" && cardNumber != "" && userName == "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => c.C_icno == cardNumber);
            }
            else if (roomId == "" && cardNumber == "" && userName != "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => c.C_username == userName);
            }
            else if (roomId != "" && cardNumber != "" && userName == "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => (c.C_roomid == roomId) && (c.C_icno == cardNumber));
            }
            else if (roomId == "" && cardNumber != "" && userName != "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => (c.C_icno == cardNumber) && (c.C_username == userName));
            }
            else if (roomId != "" && cardNumber == "" && userName != "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => (c.C_roomid == roomId) && (c.C_username == userName));
            }
            else if (roomId != "" && cardNumber != "" && userName != "")
            {
                cards.Filter = new PredicateItemFilter<Iccard>(c => (c.C_roomid == roomId) && (c.C_icno == cardNumber) && (c.C_username == userName));
            }
        }

        private void BtnCardAdd_Click(object sender, EventArgs e)
        {
            DialogCardAdd addcard = new DialogCardAdd(db);
            addcard.ShowDialog();
            RefreshCardList();
        }

        private void BtnCardMod_Click(object sender, EventArgs e)
        {
            if (iccardBindingSource.Current != null)
            {
                Iccard card = ((ObjectView<Iccard>)iccardBindingSource.Current).Object;
                DialogCardAdd addcard = new DialogCardAdd(db, card);
                addcard.ShowDialog();
                BtnRefresh_Click(this, e);
            }
            else
                MessageBox.Show("Choose the item you want to modify");
        }

        private void BtnCardDel_Click(object sender, EventArgs e)
        {
            if (iccardDataGridView.SelectedRows.Count > 0)
            {
                var cards = (from DataGridViewRow row in iccardDataGridView.SelectedRows
                             select ((ObjectView<Iccard>)row.DataBoundItem).Object as Iccard);
                foreach (var card in cards)
                {
                    var maps = (from m in db.Icmaps
                                where m.C_icno == card.C_icno
                                select m).ToList();
                    if (maps != null)
                    {
                        db.Icmaps.RemoveRange(maps);
                    }
                    db.Iccards.Remove(card);
                }
                db.SaveChanges();
                RefreshCardList();
            }
        }

        private void BtnSyncMsg_Click(object sender, EventArgs e)
        {
            DialogSelectEntranceDevices dlg = new DialogSelectEntranceDevices();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                List<Device> Devices = dlg.GetReturnResult();
                DialogUpdateCardListBox dlgUpdate = new DialogUpdateCardListBox(Devices);
                dlgUpdate.ShowDialog();
            }
        //    //***create xml***
        //    ICMDBContext db = new ICMDBContext();
        //    SaveFileDialog sa = new SaveFileDialog();
        //    string ro = "";
        //    var DeviceIds = (from m in db.icmaps
        //                     select m.C_entrancedoor).Distinct().ToList();
        //    for (int i = 0; i < DeviceIds.Count; i++)
        //    {
        //        ro = DeviceIds[i];
        //        XmlDocument xmlDoc = new XmlDocument();
        //        XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "");
        //        xmlDoc.PrependChild(xmlDecl);
        //        XmlElement nodeElement = xmlDoc.CreateElement("CardList");
        //        nodeElement.SetAttribute("ver", "00000");
        //        xmlDoc.AppendChild(nodeElement);
        //        XmlElement nodeElement2 = xmlDoc.CreateElement("dev");
        //        //var queryDev = (from foo in db.Devices where foo.roomid == DoorPool[i] select foo).FirstOrDefault();
        //        var queryDev = db.Devices.Where(s => s.roomid == ro).FirstOrDefault();
        //        nodeElement2.SetAttribute("ty", queryDev.type.ToString());
        //        nodeElement2.SetAttribute("ro", queryDev.roomid);

        //        nodeElement.AppendChild(nodeElement2);
        //        var queryCard = db.icmaps.Where(s => s.C_entrancedoor == ro);
        //        foreach (var toXml in queryCard)
        //        {
        //            XmlElement elemDevice = xmlDoc.CreateElement("card");
        //            elemDevice.InnerText = toXml.C_icno;
        //            nodeElement2.AppendChild(elemDevice);
        //        }
        //        xmlDoc.Save(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @".\data\CardXML\" + ro + ".xml");
        //    }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgFile = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FileName = "cardlist.xml"
            };
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                //加载XML文件
                XDocument doc = XDocument.Load(dlgFile.FileName);
                //获得根节点下dev子节点
                var DeviceNodes = doc.Root.Elements("dev");
                foreach (var DeviceNode in DeviceNodes)
                {
                    //int DeviceType = int.Parse(DeviceNode.Attribute("ty").Value);
                    string DeviceId = DeviceNode.Attribute("ro").Value;
                    var DeviceInDB = (from d in db.Devices
                                      where d.roomid == DeviceId
                                      select d).FirstOrDefault();
                    //if (DeviceInDB != null)
                    {
                        var cards = (from card in DeviceNode.Elements("card")
                                     select new Iccard { C_icno = card.Value, C_roomid = "00-00-00-00-00" }).ToList();
                        //using (var db = new ICMDBContext())
                        {
                            foreach (var card in cards)
                            {
                                if (DeviceInDB != null)
                                {
                                    var mapInDB = (from m in db.Icmaps
                                                   where m.C_icno == card.C_icno
                                                      && m.C_entrancedoor == DeviceId
                                                   select m).FirstOrDefault();
                                    if (mapInDB == null)
                                    {
                                        db.Icmaps.Add(new icmap { C_icno = card.C_icno, C_entrancedoor = DeviceId });
                                    }
                                }

                                var cardInDB = (from c in db.Iccards
                                                where c.C_icno == card.C_icno
                                                select c).FirstOrDefault();
                                if (cardInDB == null)
                                {
                                    db.Iccards.Add(card);
                                }
                            }
                        }
                        db.SaveChanges();
                    }
                }
                BtnRefresh_Click(null, null);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgFile = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FileName = "cardlist.xml"
            };
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                XDocument doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("CardList", new XAttribute("ver", "1.0")));
                var maps = from m in db.Icmaps
                           join d in db.Devices on m.C_entrancedoor equals d.roomid
                           orderby m.C_entrancedoor
                           select new { DeviceAddress = d.roomid, DeviceType = d.type, CardNumber = m.C_icno };
                string DeviceAddress = "";
                XElement xElement = null;
                foreach (var map in maps)
                {
                    if (DeviceAddress != map.DeviceAddress)
                    {
                        xElement = new XElement("dev", new XAttribute("ty", map.DeviceType), new XAttribute("ro", map.DeviceAddress));
                        doc.Element("CardList").Add(xElement);
                        DeviceAddress = map.DeviceAddress;
                    }
                    xElement.Add(new XElement("card", map.CardNumber));
                }
                doc.Save(dlgFile.FileName);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            RefreshCardList();
        }

        static readonly string[] CardType =
        {
            strings.Resident,   // "住户"
            strings.Staff,      // "工作人员"
            strings.Manager,    // "管理人员"
            strings.TmpCard,    // "临时卡"
            strings.OtherType   // "其他类型"
        };

        private void IccardDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTextBoxCardType.Index)
            {
                if (e.Value != null)
                {
                    int cardType = (int)e.Value;
                    if (cardType < CardType.Count())
                        e.Value = CardType[cardType];
                }
            }
            else if (e.ColumnIndex == dgvTextBoxRoomId.Index)
            {
                string roomId = e.Value as string;
                e.Value = DevicesAddressConverter.RoToChStr(roomId);
            }
            else if (e.ColumnIndex == dgvTextBoxEnabled.Index)
            {
                int enabled = (e.Value == null) ? 0 : (int)e.Value;
                e.Value = (enabled != 0) ? strings.IsWorking : strings.NotWorking;
            }
        }

        private void IccardBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (iccardBindingSource.Current == null)
                return;
            Iccard card = ((ObjectView<Iccard>)iccardBindingSource.Current).Object;

            icmapBindingSource.DataSource = db.Icmaps.Where(m => m.C_icno == card.C_icno).ToList();
        }

        private void IcmapDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string DeviceId = e.Value as string;
            e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
        }
    }
}
