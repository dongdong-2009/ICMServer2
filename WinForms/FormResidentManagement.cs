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
    public partial class FormResidentManagement : Form
    {
        ICMDBContext db = new ICMDBContext();
        public FormResidentManagement()
        {
            InitializeComponent();
        }

        private void FormResidentManagement_Load(object sender, EventArgs e)
        {
            listViewResidentInfo.View = View.Details;
            listViewResidentInfo.GridLines = false;
            listViewResidentInfo.FullRowSelect = true;
            listViewResidentInfo.Scrollable = true;
            listViewResidentInfo.CheckBoxes = true;
            listViewResidentInfo.Columns.Add(strings.residentName, 100);//"住户姓名", 100);
            listViewResidentInfo.Columns.Add(strings.residentRo, 100);//"房间号", 100);
            listViewResidentInfo.Columns.Add(strings.residentGender, 40);//"性別", 40);
            listViewResidentInfo.Columns.Add(strings.residentLeader, 40);//"户主", 40);
            listViewResidentInfo.Columns.Add(strings.residentPhone, 100);//"电话", 100);
            listViewResidentInfo.Columns.Add("ID", 0);
            //listViewResidentInfo.Columns.Add(strings.residentPID, 100);//"身分证号", 100);
            //listViewResidentInfo.Columns.Add(strings.residentBirth, 100);//"生日", 100);

            RefreshListview();
        }

        private void RefreshListview()
        {
            //***load resident data from database***///
            listViewResidentInfo.Items.Clear();

            if (db != null)
            {
                foreach (var resident in db.Holderinfoes)
                {
                    List<string> sublist = new List<string>
                    {
                        resident.C_name,
                        resident.C_roomid,
                        DecideSex(resident.C_sex),
                        DecideIsResident(resident.C_isholder),
                        resident.C_phoneno,
                        resident.C_id.ToString()
                    };
                    //sublist.Add(resident.p .PID);
                    //sublist.Add(resident.birth.Date.ToLongDateString());
                    listViewResidentInfo.Items.Add(new ListViewItem(sublist.ToArray()));
                }
            }
            //***load resident data from database***///
        }

        private string DecideIsResident(int? IsResident)
        {
            string type = "";
            switch (IsResident)
            {
                case 0:
                    type = "是";
                    break;

                case 1:
                    type = "否";
                    break;
            }
            return type;
        }

        private string DecideSex(int? sex)
        {
            string type = "";
            switch (sex)
            {
                case 0:
                    type = "男";
                    break;

                case 1:
                    type = "女";
                    break;
            }
            return type;
        }

        private void BtnAddNewHolder_Click(object sender, EventArgs e)
        {
            DialogAddResident dlg = new DialogAddResident();
            dlg.ShowDialog();
            RefreshListview();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            int nCount = listViewResidentInfo.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewResidentInfo.Items[i].Checked)
                {
                    int queryID = int.Parse(listViewResidentInfo.Items[i].SubItems[5].Text);
                    if (db != null)
                    {
                        var QueryDel = from aa in db.Holderinfoes
                                       where aa.C_id == queryID
                                       select aa;
                        foreach (var del in QueryDel)
                        {
                            db.Holderinfoes.Remove(del);
                        }
                        db.SaveChanges();
                    }
                }
            }
            checkBoxAllSelect.Checked = false;
            RefreshListview();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int isResident = ComboBoxIsResident.SelectedIndex;
            listViewResidentInfo.Items.Clear();
            if (ComboBoxIsResident.SelectedIndex == -1)
            {
                RefreshListview();
            }
            else
            {
                if (db != null)
                {
                    if (textBoxName.Text != "")
                    {
                        var query = from a in db.Holderinfoes
                                    where a.C_isholder == isResident
                                       && a.C_name.StartsWith(textBoxName.Text)
                                    select a;
                        foreach (var resident in query)
                        {
                            List<string> sublist = new List<string>
                            {
                                resident.C_name,
                                resident.C_roomid,
                                DecideSex(resident.C_sex),
                                DecideIsResident(resident.C_isholder),
                                resident.C_phoneno,
                                resident.C_id.ToString()
                            };
                            //sublist.Add(resident.PID);
                            //sublist.Add(resident.birth.Date.ToLongDateString());
                            listViewResidentInfo.Items.Add(new ListViewItem(sublist.ToArray()));
                        }
                    }
                    else
                    {
                        var query = from a in db.Holderinfoes
                                    where a.C_isholder == isResident
                                    select a;
                        foreach (var resident in query)
                        {
                            List<string> sublist = new List<string>
                            {
                                resident.C_name,
                                resident.C_roomid,
                                DecideSex(resident.C_sex),
                                DecideIsResident(resident.C_isholder),
                                resident.C_phoneno,
                                resident.C_id.ToString()
                            };
                            //sublist.Add(resident.PID);
                            //sublist.Add(resident.birth.Date.ToLongDateString());
                            listViewResidentInfo.Items.Add(new ListViewItem(sublist.ToArray()));
                        }
                    }

                }
                ComboBoxIsResident.SelectedIndex = -1;
                textBoxName.Text = "";
            }
        }

        private void CheckBoxAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            int nCount = listViewResidentInfo.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                listViewResidentInfo.Items[i].Checked = checkBoxAllSelect.Checked;
            }
        }
    }
}
