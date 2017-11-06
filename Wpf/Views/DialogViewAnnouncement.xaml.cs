using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogViewAnnouncement.xaml 的互動邏輯
    /// </summary>
    public partial class DialogViewAnnouncement : Window
    {
        public DialogViewAnnouncement(Announcement announcement)
        {
            InitializeComponent();
            this.DataContext = new AnnouncementViewModel(
                ServiceLocator.Current.GetInstance<IValidator<AnnouncementViewModel>>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<Announcement>>(),
                ServiceLocator.Current.GetInstance<IAnnouncementsRoomsModel>(),
                ServiceLocator.Current.GetInstance<IRoomsModel>(),
                ServiceLocator.Current.GetInstance<IDialogService>(),
                announcement);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
