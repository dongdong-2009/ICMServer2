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
using System.Windows.Forms.Integration;

namespace ICMServer
{
    public partial class WpfFormBase : Form
    {
        public WpfFormBase()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Create the ElementHost control for hosting the
            // WPF UserControl.
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            // Assign the WPF UserControl to the ElementHost control's
            // Child property.
            host.Child = CreateWpfForm();

            // Add the ElementHost control to the form's
            // collection of child controls.
            this.Controls.Add(host);
        }

        protected virtual UIElement CreateWpfForm() { return null; }
    }
}
