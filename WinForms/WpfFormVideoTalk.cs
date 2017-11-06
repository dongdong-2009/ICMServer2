using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class WpfFormVideoTalk : WpfFormBase
    {
        protected override UIElement CreateWpfForm()
        {
            return new ICMServer.WPF.Views.FormVideoTalk(this);
        }
    }
}
