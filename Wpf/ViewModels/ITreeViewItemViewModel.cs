using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ICMServer.WPF.ViewModels
{
    interface ITreeViewItemViewModel : INotifyPropertyChanged
    {
        ObservableCollection<TreeViewItemViewModel> Children { get; }
        bool HasDummyChild { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool? IsChecked { get; set; }
        TreeViewItemViewModel Parent { get; }
    }
}
