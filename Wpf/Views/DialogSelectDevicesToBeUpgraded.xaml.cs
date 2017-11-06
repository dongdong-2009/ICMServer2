using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogSelectDevicesToBeUpgraded.xaml 的互動邏輯
    /// </summary>
    public partial class DialogSelectDevicesToBeUpgraded : Window
    {
        private DialogSelectDevicesToBeUpgradedViewModel _viewModel;

        public DialogSelectDevicesToBeUpgraded(upgrade upgradeFile)
        {
            InitializeComponent();
            this._viewModel = new DialogSelectDevicesToBeUpgradedViewModel(
                ServiceLocator.Current.GetInstance<ICollectionModel<Device>>(),
                ServiceLocator.Current.GetInstance<IUpgradeTasksModel>(),
                ServiceLocator.Current.GetInstance<IDialogService>(),
                upgradeFile);
            //this._viewModel.OkClicked += _viewModel_OkClicked;
            this.DataContext = this._viewModel;
        }

        //private void _viewModel_OkClicked(object sender, EventArgs e)
        //{
        //    //if (this._viewModel.Devices.CurrentItem != null)
        //    //{
        //    //    this._device = this._viewModel.Devices.CurrentItem as Device;
        //    //}
        //}

    }
}
