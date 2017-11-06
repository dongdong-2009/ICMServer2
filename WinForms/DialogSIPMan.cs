using ICMServer.Models;
using ICMServer.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSIPMan : Form
    {
        public DialogSIPMan()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {
            DialogSyncSIP sync = new DialogSyncSIP();
            sync.ShowDialog();
        }

        private void SIPMan_Load(object sender, EventArgs e)
        {
            listViewSIPAccount.View = View.Details;
            listViewSIPAccount.GridLines = false;
            listViewSIPAccount.FullRowSelect = true;
            listViewSIPAccount.Scrollable = true;
            listViewSIPAccount.CheckBoxes = true;
            listViewSIPAccount.Columns.Add("Account", 110);
            listViewSIPAccount.Columns.Add("Room Address", 200);
            listViewSIPAccount.Columns.Add("Group", 150);
        }

        // 云服務管理 - 查询 
        // 查询该房号下的 SIP Accounts
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            listViewSIPAccount.Items.Clear();

            using (var db = new ICMDBContext())
            {
                string Address = "";
                string room = "";
                {
                    Address = DevicesAddressConverter.ChStrToRo(maskedTextBoxRoom.Text);
                    if (Address == "")
                        return;
                    var SIPQuery = from sip in db.Sipaccounts
                                   where sip.C_room == Address
                                   select sip;
                    foreach (var sip in SIPQuery)
                    {
                        room = sip.C_room;//DevicesAddressConverter.RoToChStr(sip.C_room);//sip.room;
                        List<string> sublist = new List<string>();
                        sublist.Add(sip.C_user);
                        sublist.Add(DevicesAddressConverter.RoToChStr(room));
                        sublist.Add(sip.C_usergroup);
                        listViewSIPAccount.Items.Add(new ListViewItem(sublist.ToArray()));
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string GroupID, RoomID = "";
            RoomID = maskedTextBoxRoom.Text;
            GroupID = DevicesAddressConverter.ChStrToRo(RoomID);
            GroupID = GroupID.Replace("-", "");
            if (GroupID == "")
            {
                return;
            }
            DialogSIPAdd addsip = new DialogSIPAdd(RoomID, GroupID);
            addsip.ShowDialog();
            BtnQuery_Click(this, e);
        }

        private async void BtnCheck_Click(object sender, EventArgs e)
        {
            string SipServer = Config.Instance.SIPServerIP;
            using (var db = new ICMDBContext())
            { 
                int nCount = listViewSIPAccount.Items.Count;
                for (int i = 0; i < nCount; i++)
                {
                    if (listViewSIPAccount.Items[i].Checked)
                    {
                        string account = listViewSIPAccount.Items[i].SubItems[0].Text;
                        string changeLine = "\r\n";
                        string toQRGen = account + changeLine;
                        var sipAccount = (from sip in db.Sipaccounts
                                            where sip.C_user == account
                                            select sip).FirstOrDefault();
                        if (sipAccount != null)
                        {
                            switch (Config.Instance.CloudSolution)
                            {
                            case CloudSolution.SIPServer:
                                toQRGen += sipAccount.C_password + changeLine + SipServer;
                                break;

                            case CloudSolution.PPHook:
                                string icmServerIP = Config.Instance.OutboundIP;
                                string result = await HttpClient.GetPPHookServiceToken("8001");
                                PPHookServiceToken pphookServiceToken = JsonConvert.DeserializeObject<PPHookServiceToken>(result);
                                string token = pphookServiceToken.token;
                                string userGroup = sipAccount.C_usergroup;

                                toQRGen = sipAccount.C_user + "\n" +
                                          sipAccount.C_password + "\n" +
                                          icmServerIP + "\n" +
                                          token + "\n" +
                                          userGroup;
                                break;
                            }
                        }
                        DialogShowQR qr = new DialogShowQR(toQRGen);
                        qr.ShowDialog();
                    }
                }
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                int nCount = listViewSIPAccount.Items.Count;
                for (int i = 0; i < nCount; i++)
                {
                    if (listViewSIPAccount.Items[i].Checked)
                    {
                        string queryAccount = listViewSIPAccount.Items[i].SubItems[0].Text;
                        if (db != null)
                        {
                            var QueryDel = from aa in db.Sipaccounts
                                           where aa.C_user == queryAccount
                                           select aa;
                            foreach (var del in QueryDel)
                            {
                                db.Sipaccounts.Remove(del);
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
            BtnQuery_Click(this, e);
        }
    }
}
