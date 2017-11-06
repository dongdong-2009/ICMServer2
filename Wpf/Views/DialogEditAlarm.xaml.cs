using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogEditAlarm.xaml 的互動邏輯
    /// </summary>
    public partial class DialogEditAlarm : Window
    {
        public DialogEditAlarm(eventwarn alarm)
        {
            InitializeComponent();

            this.DataContext = new AlarmViewModel(
                ServiceLocator.Current.GetInstance<IValidator<AlarmViewModel>>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<eventwarn>>(),
                alarm);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((AlarmViewModel)this.DataContext).ValidateCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
