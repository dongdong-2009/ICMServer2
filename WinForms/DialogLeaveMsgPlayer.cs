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
    public partial class DialogLeaveMsgPlayer : Form
    {
        string m_filename;
        public DialogLeaveMsgPlayer(string filename)
        {
            InitializeComponent();
            m_filename = filename;
        }

        private void LeaveMsgPlayer_Load(object sender, EventArgs e)
        {
            trackBarSpeaker.Maximum = 100;
            trackBarSpeaker.Value = NativeMethods.Dll_GetVolExport();
            HandleRef handleref = new HandleRef(pictureBoxPlayer, pictureBoxPlayer.Handle);
            NativeMethods.Dll_Player(m_filename, handleref.Handle);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            NativeMethods.Dll_ClosePlayer();
            this.Close();
        }

        private void TrackBarSpeaker_MouseUp(object sender, MouseEventArgs e)
        {
            NativeMethods.Dll_SetVolExport((IntPtr)trackBarSpeaker.Value);
        }
    }
}
