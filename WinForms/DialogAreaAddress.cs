using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogAreaAddress : Form
    {
        ICMDBContext db = new ICMDBContext();

        public string ReturnValue { get; set; }

        public DialogAreaAddress()
        {
            InitializeComponent();
        }

        private void DialogAreaAddress_Load(object sender, EventArgs e)
        {
            InitializeTreeView();
            //TreeViewRefresh();
        }

        //private void TreeViewRefresh()
        //{
        //    treeViewDev.BeginUpdate();
        //    treeViewDev.Nodes.Clear();
        //    int ini = 1;
        //    TreeNode root = new TreeNode();
        //    root.Text = "All Area";
        //    treeViewDev.Nodes.Add(root);
        //    using (var db = new ICMDBContext())
        //    {
        //        var RoomOrder = from Device in db.Devices
        //                        orderby Device.roomid
        //                        select Device;
        //        string[] NodeCompare = new string[6];
        //        foreach (var dev in RoomOrder)
        //        {
        //            string[] nodename = 
        //            { 
        //                strings.qu,
        //                strings.dong,
        //                strings.unit,
        //                strings.ceng,
        //                strings.shi
        //            };  // "区", "栋", "单元", "层", "室"};
        //            /**
        //             * seperate roomid may be a bit slow, 
        //             * could select 12 45 78 to use instead
        //             **/
        //            string[] RoSplit = dev.roomid.Split('-');

        //            treeViewDev.SelectedNode = root;//node start position
        //            //initial node
        //            if (ini == 1)
        //            {
        //                for (int i = 0; i < 5; i++)//6; i++)
        //                {
        //                    TreeNode tn = new TreeNode();
        //                    tn.Text = RoSplit[i] + nodename[i];
        //                    treeViewDev.SelectedNode.Nodes.Add(tn);
        //                    treeViewDev.SelectedNode = tn;
        //                    NodeCompare[i] = tn.Text;//set compare datum
        //                }
        //                ini++;
        //            }
        //            else
        //            {
        //                /**
        //                 * Go through every level comparing and adding
        //                 **/
        //                bool NotEqual = false;
        //                for (int i = 0; i < 5; i++)//6; i++)
        //                {
        //                    TreeNode tmp = new TreeNode();
        //                    //Check Node wheather exist
        //                    if (NotEqual || NodeCompare[i] != RoSplit[i] + nodename[i])
        //                    {
        //                        NotEqual = true;
        //                        tmp.Text = RoSplit[i] + nodename[i];
        //                        treeViewDev.SelectedNode.Nodes.Add(tmp);//create new node
        //                        treeViewDev.SelectedNode = tmp;
        //                        NodeCompare[i] = tmp.Text;//set compare datum
        //                    }
        //                    else
        //                    {
        //                        treeViewDev.SelectedNode = treeViewDev.SelectedNode.LastNode;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    treeViewDev.EndUpdate();
        //    //treeAfterSelect = true;
        //}

        private void InitializeTreeView()
        {
            treeViewDev.BeginUpdate();
            treeViewDev.Nodes.Clear();

            TreeNode root = new TreeNode
            {
                Text = "All Area"
            };
            root.Nodes.Add("");
            treeViewDev.Nodes.Add(root);

            treeViewDev.BeforeExpand += TreeViewDev_BeforeExpand;
            treeViewDev.EndUpdate();
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //Task.Run(() => { db.Devices.LoadAsync(); });
            //stopwatch.Stop();
            //DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
            ExpandNode(root);
        }

        private void ExpandNode(TreeNode parentNode)
        {
            string Path = (parentNode.Tag != null) 
                        ? (parentNode.Tag + "-")
                        : "";

            //using (var db = new ICMDBContext())
            {
                var groupedAddressData = 
                    (from Device in db.Devices.AsEnumerable()
                    where Device.roomid.Substring(0, Path.Length) == Path
                    && Device.roomid.Substring((parentNode.Level) * 3, 2) != "00"
                    orderby Device.roomid
                    group Device by Device.roomid.Substring((parentNode.Level) * 3, 2) into address
                    select new
                    {
                        Key = address.Key,
                        Count = address.Count()
                    });

                treeViewDev.BeginUpdate();
                parentNode.Nodes.Clear();
                foreach (var address in groupedAddressData)
                {
                    TreeNode node = new TreeNode(address.Key);
                    node.Tag = node.ToolTipText = Path + address.Key;
                    parentNode.Nodes.Add(node);
                    node.Text = node.ToolTipText = address.Key + SeparateTexts[node.Level];
                    if (node.Level < 5)
                        node.Nodes.Add("");
                }
                treeViewDev.EndUpdate();
            }
        }

        void TreeViewDev_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode parentNode = (TreeNode)e.Node;
            ExpandNode(parentNode);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private readonly string[] SeparateTexts =
        {
            "",
            strings.qu,
            strings.dong,
            strings.unit,
            strings.ceng,
            strings.shi
        };

        private void TreeViewDev_MouseDown(object sender, MouseEventArgs e)
        {
            int i;
            TreeNode selectedNode = treeViewDev.GetNodeAt(e.X, e.Y);
            if (selectedNode == null)
                return;
            string[] split = new string[6];
            split = selectedNode.FullPath.Split('\\');
            ReturnValue = "";
            for (i = 1; i < split.Count(); ++i)
            {
                if (i < split.Count() - 1)
                    ReturnValue += split[i].Trim(SeparateTexts[i].ToCharArray()) + "-";
                else
                    ReturnValue += split[i].Trim(SeparateTexts[i].ToCharArray());
            }
        }
    }
}
