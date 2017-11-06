using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogSelectDevices.xaml 的互動邏輯
    /// </summary>
    public partial class DialogSelectDevices : Window
    {
        // TODO: 如何記錄之前選擇的 device list 並恢復選擇狀態?
        public DialogSelectDevices()
        {
            InitializeComponent();
            this.DataContext = new DialogSelectDevicesViewModel(
                ServiceLocator.Current.GetInstance<ICollectionModel<Device>>());
        }
    }
}
