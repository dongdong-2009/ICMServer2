using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormAnnouncementManagement.xaml 的互動邏輯
    /// </summary>
    public partial class FormAnnouncementManagement : UserControl
    {
        public FormAnnouncementManagement()
        {
            InitializeComponent();
            //Messenger.Default.Register<ShowAddAdvertisementDialogMessage>(this, (msg) =>
            //{
            //    DialogAddAdvertisement dlg = new DialogAddAdvertisement();
            //    dlg.ShowDialog();
            //});
            //Messenger.Default.Register<ShowEditAdvertisementsDialogMessage>(this, (msg) =>
            //{
            //    DialogEditAdvertisements dlg = new DialogEditAdvertisements();
            //    dlg.ShowDialog();
            //});
            //Messenger.Default.Register<ShowViewAnnouncementDialogMessage>(this, (msg) =>
            //{
            //    if (msg.AnnouncementToBeDisplayed != null)
            //    {
            //        DialogViewAnnouncement dlg = new DialogViewAnnouncement(msg.AnnouncementToBeDisplayed);
            //        dlg.ShowDialog();
            //    }
            //});
        }
    }
}
