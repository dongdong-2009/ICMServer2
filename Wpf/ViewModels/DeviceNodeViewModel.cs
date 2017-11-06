using GalaSoft.MvvmLight.Threading;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class DeviceNodeViewModel : TreeViewItemViewModel
    {
        string _deviceAddress;
        //ObservableCollection<Device> _allDevices;
        int _maxDeviceAddressLength = 17;
        protected readonly TreeViewViewModel<TreeViewItemViewModel> _tree;
        private readonly ICollectionModel<Device> _dataModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <param name="parent"></param>
        /// <param name="deviceAddress">Complete Device Address, ex. 01-02-03 for a level 3 node</param>
        public DeviceNodeViewModel(
            TreeViewViewModel<TreeViewItemViewModel> tree,
            ICollectionModel<Device> dataModel,
            //ObservableCollection<TreeViewItemViewModel> selectedItems,
            DeviceNodeViewModel parent, 
            string deviceAddress,
            int maxDeviceAddressLength)
            : base(tree,/*selectedItems, */parent, (deviceAddress == null) || (deviceAddress.Trim().Length < maxDeviceAddressLength))
        {
            this._tree = tree;
            this._dataModel = dataModel;
            this._deviceAddress = deviceAddress;
            this._maxDeviceAddressLength = maxDeviceAddressLength;
        }

        private void LoadAllDevices()
        {
            this._dataModel.RefillDataAsync().Wait();
        }

        public string DeviceAddress
        {
            get { return this._deviceAddress; }
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "";

                if (this.Set(ref _deviceAddress, value.Trim()))
                {
                    //this.RaisePropertyChanged(() => ShortDeviceAddress);
                }
            }
        }

        protected bool IsChildrenLoaded = false;
        protected override void LoadChildren()
        {
            //Task.Run(() =>
            {
                int sublength = this.Parent == null ? 2 : 3;
                if (this.DeviceAddress.Length >= _maxDeviceAddressLength)
                    return;

                if (this._dataModel.Data.Count == 0)
                    LoadAllDevices();

                if (this._dataModel.Data.Count == 0)
                    return;

                var deviceNodes = //from device in await dataService.SelectAsync((d) => d.roomid.Substring(0, this.DeviceAddress.Length) == DeviceAddress)
                    from device in this._dataModel.Data
                    where device.roomid.Substring(0, this.DeviceAddress.Length) == DeviceAddress
                    group device by device.roomid.Substring(0, this.DeviceAddress.Length + sublength) into address
                    orderby address.Key
                    select new
                    {
                        Key = address.Key,
                        Count = address.Count()
                    };
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    foreach (var d in deviceNodes)
                        this.Children.Add(new DeviceNodeViewModel(_tree, _dataModel, /*this._selectedItems, */this, d.Key, _maxDeviceAddressLength));
                });
            }
            //);
        }
    }
}
