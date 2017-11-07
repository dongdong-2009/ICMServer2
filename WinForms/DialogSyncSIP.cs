using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSyncSIP : Form
    {
        ICMDBContext m_DB = new ICMDBContext();
        string m_SipServerAndPort;
        string m_AddRingGroup; //= "http://sp.ite.com.tw:5050/AddRingGroup?group_num=";
        HashSet<string> m_RingGroups = new HashSet<string>();
        List<sipaccount> m_SipAccounts;

        public DialogSyncSIP()
        {
            InitializeComponent();
        }

        private void BtnStartSync_Click(object sender, EventArgs e)
        {
            // TODO: 把整個行为丟到 background thread 去做
            if (Config.Instance.SIPServerIP.Length == 0 || Config.Instance.SIPServerPort == 0)
            {
                // TODO: 提示 sip server 的資訊尚未输入
                return;
            }

            // http://172.29.1.21:5050/AddSipAccount?sip_account=account1&sip_pwd=BqVXrqD0YS&apply=1
            // ring group
            // account1-account2-account3
            // http://172.29.1.21:5050/AddRingGroup?group_num=0000000000&group_list=account1-account2-account3
            foreach (var account in m_SipAccounts)
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create("http://" + m_SipServerAndPort + "/AddSipAccount?sip_account=" + account.C_user + "&sip_pwd=" + account.C_password + "&apply=0");
                //http://sp.ite.com.tw:5050/AddSipAccount?sip_account=" + result.user + "&sip_pwd=" + result.passwd + "&apply=0");
                request.Method = WebRequestMethods.Http.Get;
                request.ContentType = "";
                request.Timeout = 1000 * 5;
                try
                {
                    var resp = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                    if (resp.ToString() != "0")
                    {
                        MessageBox.Show("Add SIP Account Failed!");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Time out!");
                    return;
                }
            }

            // Add ring group
            foreach (var ringGroup in m_RingGroups)
            {
                string httpUrl = m_AddRingGroup + ringGroup + "&group_list=";
                string groupNum = ringGroup;
                var QueryGroup = from sip in m_DB.Sipaccounts
                                 where sip.C_usergroup == groupNum
                                 select sip;
                foreach (var result in QueryGroup)
                {
                    httpUrl += result.C_user + "-";
                    result.C_sync = 1;
                }
                httpUrl = httpUrl.Substring(0, httpUrl.Length - 1);
                WebRequest request = (HttpWebRequest)WebRequest.Create(httpUrl);
                request.Method = WebRequestMethods.Http.Get;
                request.ContentType = "";
                request.Timeout = 1000 * 5;
                try
                {
                    var resp = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                    if (resp.ToString() != "0")
                    {
                        MessageBox.Show("Add Ring Group Failed!");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Time out!");
                    return;
                }
            }

            // Apply all
            WebRequest apply = (HttpWebRequest)WebRequest.Create("http://" + m_SipServerAndPort + "/ApplySipAccount");
            //"http://sp.ite.com.tw:5050/ApplySipAccount");
            apply.Method = WebRequestMethods.Http.Get;
            apply.ContentType = "";
            apply.Timeout = 1000 * 5;
            try
            {
                var applyresp = new StreamReader(apply.GetResponse().GetResponseStream()).ReadToEnd();
                if (applyresp.ToString() != "100")
                {
                    MessageBox.Show("Apply SIP Account Failed!");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Time out!");
                return;
            }

            
            m_DB.SaveChanges();
            MessageBox.Show(strings.SyncFinish);//"同步完成!");
            this.Close();
        }

        private void DialogSyncSIP_Load(object sender, EventArgs e)
        {
            if (Config.Instance.SIPServerIP.Length == 0 || Config.Instance.SIPServerPort == 0)
            {
                // TODO: 提示 sip server 的資訊尚未输入
                this.Close();
                return;
            }
            m_SipServerAndPort = Config.Instance.SIPServerIP + ":" + Config.Instance.SIPServerPort.ToString();
            m_AddRingGroup = "http://" + m_SipServerAndPort + "/AddRingGroup?group_num=";
            listViewSync.View = View.Details;
            listViewSync.GridLines = false;
            listViewSync.FullRowSelect = true;
            listViewSync.Scrollable = true;
            listViewSync.CheckBoxes = false;
            listViewSync.Columns.Add("SIP Account", 110);
            listViewSync.Columns.Add("SIP Status", 110);
            // 列出兩栏，acount和status

            // 找出所有尚未同步的帳号
            m_SipAccounts = (from a in m_DB.Sipaccounts
                             where a.C_sync == 0
                             select a).ToList();
            foreach (var account in m_SipAccounts)
            {
                List<string> sublist = new List<string>
                {
                    account.C_user,    // 帳号名稱
                    "unsync"          // 顯示未同步
                };
                listViewSync.Items.Add(new ListViewItem(sublist.ToArray()));

                if (account.C_usergroup.Length > 0)
                    m_RingGroups.Add(account.C_usergroup);
            }
        }
    }
}
