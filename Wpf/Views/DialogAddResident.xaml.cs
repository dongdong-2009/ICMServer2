using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddResident.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddResident : Window
    {
        public DialogAddResident()
        {
            InitializeComponent();
            this.DataContext = new ResidentViewModel(
                ServiceLocator.Current.GetInstance<IValidator<ResidentViewModel>>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<holderinfo>>());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((ResidentViewModel)this.DataContext).ValidateCommand.Execute(null);
        }
    }
}
