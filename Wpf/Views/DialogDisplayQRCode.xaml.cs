using ICMServer.WPF.ViewModels;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogDisplayQRCode.xaml 的互動邏輯
    /// </summary>
    public partial class DialogDisplayQRCode : Window
    {
        public DialogDisplayQRCode(string content)
        {
            InitializeComponent();
            this.DataContext = new DialogDisplayQRCodeViewModel(content);
        }
    }
}
