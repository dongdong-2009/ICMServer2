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
    public partial class DialogCardAdd : Form
    {
        ICMDBContext db;
        int id = 0;
        Iccard m_Card;

        public DialogCardAdd(ICMDBContext db, Iccard card = null)
        {
            InitializeComponent();
            this.db = db;
            m_Card = card;
        }

        private void AddCard_Load(object sender, EventArgs e)
        {
            ComboBoxOnUse.SelectedIndex = 1;    //默认有效
            ComboBoxIsResident.SelectedIndex = 0;
            listViewUnRelate.Columns.Add(strings.UnrelatedDev, 189);//"未开聯设备", 189);
            listViewRelate.Columns.Add(strings.RelatedDev, 206);//"开聯设备", 206);

            // Moding mode
            if (m_Card != null)
            {
                //using (ICMDBContext db = new ICMDBContext())
                {
                    List<string> IdPool = new List<string>(); 
                    
                    var card = (from c in db.Iccards
                                where c.C_icno == m_Card.C_icno
                                select c).FirstOrDefault();
                    
                    m_Card = card;

                    id = card.C_icid;
                    textBoxICCard.Text = card.C_icno;
                    textBoxRo.Text = card.C_roomid;
                    textBoxResidentName.Text = card.C_username;
                    ComboBoxIsResident.SelectedIndex = (int)(card.C_ictype ?? 0);
                    ComboBoxOnUse.SelectedIndex = (int)(card.C_available ?? 0);
                    checkBoxStartDate.Checked = (card.C_uptime != null);
                    checkBoxEndDate.Checked = (card.C_downtime != null);
                    dateTimePickerStart.Value = (card.C_uptime != null) ? card.C_uptime.Value : DateTime.Now;
                    dateTimePickerEnd.Value = (card.C_downtime != null) ? card.C_downtime.Value : DateTime.Now;

                    var relatedDevices = (from icno in db.Icmaps
                                          where icno.C_icno == m_Card.C_icno
                                          select icno).ToArray();
                    foreach (var dev in relatedDevices)
                    {
                        List<string> sublist = new List<string>();
                        string room = DevicesAddressConverter.RoToChStr(dev.C_entrancedoor);
                        sublist.Add(room);
                        sublist.Add(dev.C_id.ToString());
                        // 开聯设备
                        listViewRelate.Items.Add(new ListViewItem(sublist.ToArray()));

                        IdPool.Add(dev.C_entrancedoor);
                    }

                    var queryEntrance = from foo in db.Devices
                                        where foo.type < (int)DeviceType.Indoor_Phone && foo.type > (int)DeviceType.Control_Server
                                        select foo;
                    foreach (var dev in queryEntrance)
                    {
                        bool equal = false;
                        foreach (var a in IdPool)
                        {
                            if (a == dev.roomid)
                            {
                                equal = true;
                                break;
                            }
                        }
                        if (equal)
                            continue;
                        List<string> sublist = new List<string>();
                        string room = DevicesAddressConverter.RoToChStr(dev.roomid);
                        sublist.Add(room);
                        sublist.Add(dev.id.ToString());
                        listViewUnRelate.Items.Add(new ListViewItem(sublist.ToArray()));
                    }
                }
                // Moding mode
            }
            else
            {
                InitAddNewCard();
            }
        }

        private void InitAddNewCard()
        {
            string room = "";
            textBoxICCard.ReadOnly = false;
            dateTimePickerStart.Enabled = dateTimePickerEnd.Enabled = false;
            //using (ICMDBContext db = new ICMDBContext())
            {
                var queryEntrance = from foo in db.Devices
                                    where foo.type < 5 && foo.type > 0
                                    select foo;
                foreach (var dev in queryEntrance)
                {
                    List<string> sublist = new List<string>();
                    room = DevicesAddressConverter.RoToChStr(dev.roomid);
                    sublist.Add(room);
                    sublist.Add(dev.id.ToString());
                    listViewUnRelate.Items.Add(new ListViewItem(sublist.ToArray()));
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (m_Card != null)// Moding mode
            {
                if ((textBoxRo.Text != "") && (textBoxResidentName.Text != ""))
                {
                    //using (ICMDBContext db = new ICMDBContext())
                    {
                        int lastMap = 0;
                        try
                        {
                            lastMap = db.Icmaps.OrderByDescending(p => p.C_id).Select(r => r.C_id).First();
                        }
                        catch { }
                        var card = (from c in db.Iccards
                                    where c.C_icno == m_Card.C_icno
                                    select c).FirstOrDefault();
                        if (card != null)
                        {
                            db.Iccards.Remove(card);
                            db.SaveChanges();
                            card.C_icno = textBoxICCard.Text;
                            card.C_roomid = textBoxRo.Text;
                            card.C_available = ComboBoxOnUse.SelectedIndex;
                            card.C_ictype = ComboBoxIsResident.SelectedIndex;
                            card.C_username = textBoxResidentName.Text;
                            if (checkBoxStartDate.Checked)
                                card.C_uptime = dateTimePickerStart.Value;
                            else
                                card.C_uptime = null;
                            if (checkBoxEndDate.Checked)
                                card.C_downtime = dateTimePickerEnd.Value;
                            else
                                card.C_downtime = null;
                            card.C_icid = id;

                            db.Iccards.Add(card);
                            db.SaveChanges();
                        }
                        // delete map
                        var map = from foo in db.Icmaps
                                  where foo.C_icno == card.C_icno
                                  select foo;
                        db.Icmaps.RemoveRange(map);
                        db.SaveChanges();
                        // del end

                        foreach (ListViewItem tmp in listViewRelate.Items)
                        {
                            string ro = tmp.SubItems[0].Text;

                            icmap MapSave = new icmap
                            {
                                C_icno = textBoxICCard.Text,
                                C_entrancedoor = DevicesAddressConverter.ChStrToRo(ro),
                                C_id = lastMap + 1
                            };
                            db.Icmaps.Add(MapSave);
                        }
                        db.SaveChanges();

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(strings.CardRoNameCannotBeNull);//"卡号、房号、姓名不得为空!");
                }
            }
            else// Adding mode
            {
                if (Check())
                {
                    //using (ICMDBContext db = new ICMDBContext())
                    {
                        int last = 0;
                        int lastMap = 0;
                        try
                        {
                            last = db.Iccards.OrderByDescending(p => p.C_icid).Select(r => r.C_icid).First();
                        }
                        catch { }
                        try
                        {
                            lastMap = db.Icmaps.OrderByDescending(p => p.C_id).Select(r => r.C_id).First();
                        }
                        catch{}
                        Iccard CardSave = new Iccard
                        {
                            C_icno = textBoxICCard.Text,
                            C_roomid = textBoxRo.Text,
                            C_available = ComboBoxOnUse.SelectedIndex,
                            C_ictype = ComboBoxIsResident.SelectedIndex,
                            C_username = textBoxResidentName.Text,
                            C_uptime = dateTimePickerStart.Value,
                            C_downtime = dateTimePickerEnd.Value,
                            C_icid = last + 1
                        };
                        foreach (ListViewItem tmp in listViewRelate.Items)
                        {
                            string ro = tmp.SubItems[0].Text;

                            icmap MapSave = new icmap
                            {
                                C_icno = textBoxICCard.Text,
                                C_entrancedoor = DevicesAddressConverter.ChStrToRo(ro),
                                C_id = lastMap + 1
                            };
                            db.Icmaps.Add(MapSave);
                        }
                        db.Iccards.Add(CardSave);
                        db.SaveChanges();
                        this.Close();
                    }
                }
            }
        }

        private bool Check()
        {
            if(textBoxICCard.Text.Length != 10)
            {
                MessageBox.Show(strings.CardCannotLessThan10);//"卡号10位");
                return false;
            }
            //using(ICMDBContext db = new ICMDBContext())
            {
                int count = (from card in db.Iccards
                             where card.C_icno == textBoxICCard.Text
                             select card).Count();
                if (count > 0)
                {
                    MessageBox.Show(strings.CardExistCannotSave);//"卡号已存在，无法保存！");
                    return false;
                }
            }
            if ((textBoxRo.Text != "") && (textBoxICCard.Text != "") && (textBoxResidentName.Text != ""))
                return true;
            else
            {
                MessageBox.Show(strings.CardRoNameCannotBeNull);//"卡号、房号、姓名不得为空!");
                return false;
            }
        }

        private void BtnToRight_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem tmp in listViewUnRelate.Items)
            {
                if (tmp.Selected == true)
                {
                    ListViewItem item;
                    string[] arr = new string[2];
                    for (int j = 0; j < 2; j++)
                        arr[j] = tmp.SubItems[j].Text;
                    item = new ListViewItem(arr);
                    //ListViewItem item = new ListViewItem(tmp.Text);
                    listViewRelate.Items.Add(item);
                    tmp.Remove();
                }
            }
        }

        private void BtnToLeft_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem tmp in listViewRelate.Items)
            {
                if (tmp.Selected == true)
                {
                    ListViewItem item;
                    string[] arr = new string[2];
                    for (int j = 0; j < 2; j++)
                        arr[j] = tmp.SubItems[j].Text;
                    item = new ListViewItem(arr);
                    //ListViewItem item = new ListViewItem(tmp.Text);
                    listViewUnRelate.Items.Add(item);
                    tmp.Remove();
                }
            }
        }

        private void BtnSelectRo_Click(object sender, EventArgs e)
        {
            DialogAreaAddress dlg = new DialogAreaAddress();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxRo.Text = dlg.ReturnValue;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBoxStartDate_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePickerStart.Enabled = checkBoxStartDate.Checked;
        }

        private void CheckBoxEndDate_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Enabled = checkBoxStartDate.Checked;
        }

        private void ComboBoxIsResident_Changed(object sender, EventArgs e)
        {
            if(ComboBoxIsResident.SelectedIndex == 2)
            {
                textBoxRo.Text = "00-00-00-00-00-01";
            }
        }
    }
}
