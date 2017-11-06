using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogImportCardList.xaml 的互動邏輯
    /// </summary>
    public partial class DialogImportCardList : Window
    {
        public DialogImportCardList(Action afterImportedCallback)
        {
            InitializeComponent();
            this.DataContext = new DialogImportCardListViewModel(
               ServiceLocator.Current.GetInstance<ICollectionModel<iccard>>(),
               ServiceLocator.Current.GetInstance<IDataService<iccard>>(),
               ServiceLocator.Current.GetInstance<IDataService<icmap>>(),
               ServiceLocator.Current.GetInstance<IDeviceDataService>(),
               ServiceLocator.Current.GetInstance<IDialogService>(),
               afterImportedCallback);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
