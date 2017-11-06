using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogEventWarn : Form
    {
        SoundPlayer audio = new SoundPlayer(Alarm._110jl);

        internal bool IsDisplaying { get; private set; }

        public DialogEventWarn()
        {
            InitializeComponent();
        }

        private void DialogEventWarn_Load(object sender, EventArgs e)
        {
            IsDisplaying = true;
            audio.PlayLooping();
            RefreshAlarmsList();
        }

        internal async void RefreshAlarmsList()
        {
            using (var db = new ICMDBContext())
            {
                var alarms = from a in db.Eventwarns
                             where a.handlestatus == 0 && a.Action == "trig"
                             orderby a.time descending
                             select a;
                eventwarnBindingSource.DataSource = await alarms.ToListAsync();
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            audio.Stop();
            this.Close();
            IsDisplaying = false;
        }

        private void eventwarnDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTextBoxAlarmSrc.Index)
            {
                string DeviceId = e.Value as string;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dgvTextBoxAlarmAction.Index)
            {
                string action = e.Value as string;
                if (action != null)
                {
                    if (action == "trig")
                        e.Value = "事件觸发";
                    else if (action == "unalarm")
                        e.Value = "事件解除";
                    else if (action == "enable")
                        e.Value = "佈防";
                    else if (action == "disable")
                        e.Value = "撤防";
                    else
                        e.Value = "";
                }
            }
        }

        //private void buttonRevocation_Click(object sender, EventArgs e)
        //{
        //    ICMDBContext db = new ICMDBContext();
        //    List<string> RoomList = new List<string>();

        //    var queryWarn = from foo in db.eventwarns
        //                    where foo.handlestatus == 0 && foo.action == "trig"
        //                    group foo by foo.srcaddr;
        //    foreach (var roGroup in queryWarn)
        //    {
        //        RoomList.Add(roGroup.Key);
        //    }

        //    foreach (var evocationRo in RoomList)
        //    {
        //        var queryIP = (from foo in db.Devices
        //                       where foo.roomid == evocationRo
        //                       select foo).FirstOrDefault();
        //        //evocation.handlestatus = 1;
        //        WebRequest request = (HttpWebRequest)WebRequest.Create("http://" + queryIP.ip + "/unguard?ro=" + evocationRo);
        //        request.Method = WebRequestMethods.Http.Get;
        //        request.ContentType = "";
        //        request.Timeout = 1000 * 5;
        //        try
        //        {
        //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //            if (response.StatusCode != HttpStatusCode.OK)
        //            {
        //                MessageBox.Show("Evocation Failed!");
        //                return;
        //            }
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Device" + evocationRo + "is offline!");
        //        }
        //    }
        //    RefreshListview();
        //    audio.Stop();
        //    this.Close();
        //}

        //private void RefreshListview()
        //{
        //    listView.Items.Clear();
        //    ICMDBContext db = new ICMDBContext();
        //    if (db != null)
        //    {
        //        var queryWarn = from foo in db.eventwarns
        //                        where foo.handlestatus == 0
        //                        select foo;
        //        foreach (var warn in queryWarn)
        //        {
        //            List<string> sublist = new List<string>();
        //            sublist.Add(warn.type);
        //            sublist.Add(warn.time.ToString());
        //            sublist.Add(warn.srcaddr);
        //            sublist.Add(warn.channel.ToString() + "channel");
        //            if (warn.handlestatus == 0)
        //                sublist.Add("未撤防");
        //            sublist.Add("已撤防");
        //            listView.Items.Add(new ListViewItem(sublist.ToArray()));
        //        }
        //    }
        //}

        
    }
}
