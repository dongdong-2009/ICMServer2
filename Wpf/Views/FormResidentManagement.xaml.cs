using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormResidentManagement.xaml 的互動邏輯
    /// </summary>
    public partial class FormResidentManagement : UserControl
    {
        public FormResidentManagement()
        {
            InitializeComponent();

            //Messenger.Default.Register<ShowAddResidentDialogMessage>(this, (msg) =>
            //{
            //    DialogAddResident dlg = new DialogAddResident();
            //    dlg.ShowDialog();
            //});
            //Messenger.Default.Register<ShowEditResidentDialogMessage>(this, (msg) =>
            //{
            //    if (msg.ResidentToBeEdited != null)
            //    {
            //        DialogEditResident dlg = new DialogEditResident(msg.ResidentToBeEdited);
            //        dlg.ShowDialog();
            //    }
            //});
        }
    }
}
