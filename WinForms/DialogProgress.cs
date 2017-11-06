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
    public partial class DialogProgress : Form
    {
        public DialogProgress()
        {
            InitializeComponent();
        }

        protected void AddLog(string format, params object[] args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { AddLog(format, args); }));
            }
            else
            {
                listBoxLog.Items.Add(string.Format("[{0}]: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format(format, args)));
            }
        }
    }
}
