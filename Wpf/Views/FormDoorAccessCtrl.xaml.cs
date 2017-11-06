using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormDoorAccessCtrl.xaml 的互動邏輯
    /// </summary>
    public partial class FormDoorAccessCtrl : UserControl
    {
        public FormDoorAccessCtrl()
        {
            InitializeComponent();
            //Messenger.Default.Register<ShowAddSecurityCardDialogMessage>(this, (msg) =>
            //{
            //    DialogAddSecurityCard dlg = new DialogAddSecurityCard();
            //    dlg.ShowDialog();
            //});
        }
    }
}
