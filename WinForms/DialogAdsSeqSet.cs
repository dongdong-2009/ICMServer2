using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogAdsSeqSet : Form
    {
        int last;
        public DialogAdsSeqSet(int pass)
        {
            InitializeComponent();
            last = pass;
        }

        private void AdsSeqSet_Load(object sender, EventArgs e)
        {
            listViewOrder.View = View.Details;
            listViewOrder.GridLines = false;
            listViewOrder.FullRowSelect = true;
            listViewOrder.Scrollable = true;
            //listViewOrder.CheckBoxes = true;
            listViewOrder.Columns.Add(strings.TimeSequence, 50);//"时序", 50);
            listViewOrder.Columns.Add(strings.AdsTitle, 200);//"广告标题", 200);
            dateTimePickerStart.Value = DateTime.Parse(Config.Instance.AdvertisementBeginTime);
            dateTimePickerEnd.Value = DateTime.Parse(Config.Instance.AdvertisementEndTime);
            listRefresh();
        }

        private void listRefresh()
        {
            listViewOrder.Items.Clear();
            ICMDBContext db = new ICMDBContext();
            var order = from ad in db.Advertisements
                        orderby ad.C_no
                        select ad;
            foreach (var list in order)
            {
                List<string> sublist = new List<string>();
                sublist.Add(list.C_no.ToString());
                sublist.Add(list.C_title);
                listViewOrder.Items.Add(new ListViewItem(sublist.ToArray()));
            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            ICMDBContext db = new ICMDBContext();
            foreach (ListViewItem tmp in listViewOrder.Items)
            {
                if (tmp.Selected == true)
                {
                    int seq = int.Parse(tmp.SubItems[0].Text);
                    if (seq > 1)    // 被选到的項目
                    {
                        var query = from zz in db.Advertisements
                                    where zz.C_no == seq || zz.C_no == seq - 1
                                    select zz;
                        foreach (var swap in query)
                        {
                            if (swap.C_no == seq)
                                swap.C_no -= 1;
                            else
                                swap.C_no += 1;
                        }
                        db.SaveChanges();
                    }
                }
            }
            listRefresh();
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            ICMDBContext db = new ICMDBContext();
            foreach (ListViewItem tmp in listViewOrder.Items)
            {
                if (tmp.Selected == true)
                {
                    int seq = int.Parse(tmp.SubItems[0].Text);
                    if (seq < last)
                    {
                        var query = from zz in db.Advertisements
                                    where zz.C_no == seq || zz.C_no == seq + 1
                                    select zz;
                        foreach (var swap in query)
                        {
                            if (swap.C_no == seq)
                                swap.C_no += 1;
                            else
                                swap.C_no -= 1;
                        }
                        db.SaveChanges();
                    }
                }
            }
            listRefresh();
        }
        //public string filename = "mm.cfg";
        //SetupIni ini = new SetupIni();
        private void BtnOk_Click(object sender, EventArgs e)
        {
            Config.Instance.AdvertisementBeginTime = dateTimePickerStart.Value.ToString("HH:mm:ss");
            Config.Instance.AdvertisementEndTime = dateTimePickerEnd.Value.ToString("HH:mm:ss");
            //ini.IniWriteValue("ADVERTISEMENT", "INDEX_AD_BEGINTIME", dateTimePickerStart.Value.ToString("HH:mm:ss"), filename);
            //ini.IniWriteValue("ADVERTISEMENT", "INDEX_AD_ENDINTIME", dateTimePickerEnd.Value.ToString("HH:mm:ss"), filename);
            //ICMDBContext db = new ICMDBContext();
            //foreach(var ads in db.LobbyAds)
            //{
            //    ads.displystart = dateTimePickerStart.Value;
            //    ads.displyend = dateTimePickerEnd.Value;
            //}
            //db.SaveChanges();
            this.Close();
        }
    }
}
