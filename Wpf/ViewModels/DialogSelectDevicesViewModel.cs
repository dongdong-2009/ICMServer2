using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.WPF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ICMServer.WPF.ViewModels
{
    public class TreeViewViewModel<TItemViewModel> : ViewModelBase where TItemViewModel : TreeViewItemViewModel
    {
        protected TreeViewItemViewModel _rootNode;
        protected TreeViewItemViewModel _selectedItem;

        public TreeViewItemViewModel SelectedItem
        {
            get
            {
                //if (_selectedItem == null)
                //{
                //    var selectedNodes = FindSelected(this._rootNode).GetEnumerator();
                //    _selectedItem = (selectedNodes.MoveNext())
                //        ? selectedNodes.Current
                //        : null;
                //}
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    if (_selectedItem != null)
                        _selectedItem.IsSelected = false;
                    _selectedItem = value;
                    _selectedItem.IsSelected = true;
                    if (_selectedItem.Parent != null)
                        _selectedItem.Parent.IsExpanded = true;
                    this.RaisePropertyChanged(() => SelectedItem);
                    this.RaisePropertyChanged(() => CheckedItems);
                }
            }
        }

        IEnumerable<TreeViewItemViewModel> FindSelected(TreeViewItemViewModel node)
        {
            if (node != null)
            {
                if (node.IsSelected)
                    yield return node;

                if (node.Children != null)
                {
                    foreach (var child in node.Children)
                        foreach (var selected in this.FindSelected(child))
                            yield return selected;
                }
            }
        }

        public List<TreeViewItemViewModel> CheckedItems
        {
            get
            {
                return FindChecked(this._rootNode).ToList();
                //var result = (selectedNodes.MoveNext())
                //    ? selectedNodes.Current
                //    : null;
                //return result;
            }
        }

        IEnumerable<TreeViewItemViewModel> FindChecked(TreeViewItemViewModel node)
        {
            if (node != null && node.Children != null)
            {
                if (node.Children.Count == 0)
                {
                    if (node.IsChecked.HasValue && node.IsChecked.Value)
                        yield return node;
                }
                else
                {
                    foreach (var child in node.Children)
                        foreach (var selected in this.FindChecked(child))
                            yield return selected;
                }
            }
        }
    }

    public class DialogSelectDevicesViewModel : TreeViewViewModel<TreeViewItemViewModel>
    {
        readonly ObservableCollection<DeviceNodeViewModel> _devicesTree;
        public ObservableCollection<DeviceNodeViewModel> DevicesTree
        {
            get { return _devicesTree; }
        }

        //readonly ObservableCollection<TreeViewItemViewModel> _selectedItems;
        //public ObservableCollection<TreeViewItemViewModel> SelectedItems => _selectedItems;
        
        string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    var matchedNodes = FindDeviceNode(value, this._rootNode as DeviceNodeViewModel).GetEnumerator();
                    var matchedNode = (matchedNodes.MoveNext())
                        ? matchedNodes.Current
                        : null;
                    if (matchedNode != null)
                    {
                        this.SelectedItem = matchedNode;
                        _address = value;
                        this.RaisePropertyChanged(() => Address);
                    }
                }
            }
        }
        
        IEnumerable<DeviceNodeViewModel> FindDeviceNode(string deviceAddress, DeviceNodeViewModel node)
        {
            if (node != null)
            {
                //DebugLog.TraceMessage(node.DeviceAddress);
                if (node.DeviceAddress == deviceAddress)    // 如果就是這個節點
                    yield return node;

                // 如果是在這節點下的子節點
                if (node.Children != null && deviceAddress.StartsWith(node.DeviceAddress))  
                {
                    foreach (var child in node.Children)
                        foreach (var selected in this.FindDeviceNode(deviceAddress, child as DeviceNodeViewModel))
                            yield return selected;
                }
            }
        }

        //readonly ObservableCollection<Device> _selectedDevices;
        //public ObservableCollection<Device> SelectedDevices => _selectedDevices;

        //protected object _lock = new object();
        //protected IDeviceDataService _dataService;

        public DialogSelectDevicesViewModel(
            ICollectionModel<Device>    dataModel, 
            int                         maxAddressLength = 17)
        {
            //this._dataService = dataService;
            //this._selectedDevices = new ObservableCollection<Device>();
            //this._selectedItems = new ObservableCollection<TreeViewItemViewModel>();
            //this._selectedItems.CollectionChanged += _selectedItems_CollectionChanged;

            this._rootNode = new DeviceNodeViewModel(this, dataModel, /*this._selectedItems, */null, "", maxAddressLength);
            this._devicesTree = new ObservableCollection<DeviceNodeViewModel>();
            this._devicesTree.Add(this._rootNode as DeviceNodeViewModel);

            //this._devicesRootNode[0].LoadChildren();
            //RefreshCommand.Execute(null);
        }

        //private bool _isSelectionChanged;
        //private void _selectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    this._isSelectionChanged = true;
        //}

        //DeferredAction _syncSelectedDevicesAction;
        //private DeferredAction SyncSelectedDevicesAction
        //{
        //    get
        //    {
        //        return _syncSelectedDevicesAction ?? (_syncSelectedDevicesAction = DeferredAction.Create(
        //            async () => await SyncSelectedDevicesAsync().ConfigureAwait(false)));
        //    }
        //}

        //public Task SyncSelectedDevicesAsync()
        //{
        //    return Task.Run(() =>
        //    {
        //        //DebugLog.TraceMessage("");
        //        lock (_selectedDevices)
        //        {
        //            try
        //            {
        //                //_devices.ReplaceRange(_dataService.SelectAll());
        //            }
        //            catch (Exception) { }
        //        }
        //        //DebugLog.TraceMessage("");
        //    });
        //}
        private RelayCommand _GetCheckedItemsCommand;
        public RelayCommand GetCheckedItemsCommand
        {
            get
            {
                return _GetCheckedItemsCommand ?? (_GetCheckedItemsCommand = new RelayCommand(
                () => 
                {
                    var items = this.CheckedItems;
                    foreach (var item in items)
                    {

                    }
                }));
            }
        }
    }
}
