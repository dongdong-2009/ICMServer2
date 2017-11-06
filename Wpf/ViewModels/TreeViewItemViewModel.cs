using GalaSoft.MvvmLight;
using ICMServer.WPF.Collections.ObjectModel;
using System.Collections.ObjectModel;

namespace ICMServer.WPF.ViewModels
{
    /// <summary>
    /// Base class for all ViewModel classes displayed by TreeViewItems.  
    /// This acts as an adapter between a raw data object and a TreeViewItem.
    /// </summary>
    /// 參考 https://www.codeproject.com/Articles/26288/Simplifying-the-WPF-TreeView-by-Using-the-ViewMode
    public class TreeViewItemViewModel : ViewModelBase, ITreeViewItemViewModel
    {
        #region Data
        static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();

        readonly ObservableCollection<TreeViewItemViewModel> _children;
        readonly TreeViewItemViewModel _parent;
        readonly TreeViewViewModel<TreeViewItemViewModel> _tree;
        //protected readonly ObservableCollection<TreeViewItemViewModel> _selectedItems;
        //protected object _lock = new object();

        bool _isExpanded;
        bool _isSelected;
        bool? _isChecked;
        #endregion // Data

        #region Constructors

        public TreeViewItemViewModel(
            TreeViewViewModel<TreeViewItemViewModel> tree,
            //ObservableCollection<TreeViewItemViewModel> selectedItems,
            TreeViewItemViewModel parent, 
            bool lazyLoadChildren)
        {
            //_selectedItems = selectedItems;
            _tree = tree;
            _parent = parent;
            if (parent == null)
                this.SetIsChecked(false, false, false);
            else
                this.SetIsChecked(parent.IsChecked ?? false, false, false);

            _children = new ObservableCollection<TreeViewItemViewModel>();
            //BindingOperations.EnableCollectionSynchronization(_children, _lock);

            if (lazyLoadChildren)
                _children.Add(DummyChild);
        }

        // This is used to create the DummyChild instance.
        private TreeViewItemViewModel()
        {
        }

        #endregion // Constructors

        #region Presentation Members

        #region Children

        /// <summary>
        /// Returns the logical child items of this object.
        /// </summary>
        public ObservableCollection<TreeViewItemViewModel> Children
        {
            get
            {
                MakeSureChildrenIsLoaded();
                return _children;
            }
        }

        #endregion // Children

        protected void MakeSureChildrenIsLoaded()
        {
            if (this.HasDummyChild)
            {
                this._children.Remove(DummyChild);
                this.LoadChildren();
            }
        }

        #region HasLoadedChildren

        /// <summary>
        /// Returns true if this object's Children have not yet been populated.
        /// </summary>
        public bool HasDummyChild
        {
            get { return this._children.Count == 1 && this._children[0] == DummyChild; }
        }

        #endregion // HasLoadedChildren

        #region IsExpanded
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// 若設定此值為 true 會導致所有父節點全部展開
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (this.Set(ref _isExpanded, value))
                {
                    // Expand all the way up to the root.
                    if (_isExpanded && _parent != null)
                        _parent.IsExpanded = true;

                    // Lazy load the child items, if necessary.
                    MakeSureChildrenIsLoaded();
                }
            }
        }
        #endregion // IsExpanded

        #region IsSelected
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (this.Set(ref _isSelected, value))
                {
                    if (_isSelected && _tree != null)
                    {
                        _tree.SelectedItem = this;
                    }
                }
            }
        }
        #endregion // IsSelected

        #region LoadChildren
        /// <summary>
        /// Invoked when the child items need to be loaded on demand.
        /// Subclasses can override this to populate the Children collection.
        /// </summary>
        protected virtual void LoadChildren()
        {
        }
        #endregion // LoadChildren

        #region Parent
        public TreeViewItemViewModel Parent
        {
            get { return _parent; }
        }
        #endregion // Parent

        #region IsChecked
        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (this.Set(() => IsChecked, ref _isChecked, value))
            {
                if (updateChildren && _isChecked.HasValue)
                    this.Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));
                if (updateParent && _parent != null)
                    _parent.VerifyCheckState();
            }
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.Children.Count; ++i)
            {
                bool? current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

        //void AddToSelectedItems()
        //{
        //    if (this._selectedItems == null)
        //        return;

        //    if (this._selectedItems.IndexOf(this) < 0)
        //    {
        //        this._selectedItems.Add(this);
        //    }
        //}

        //void RemoveFromSelectedItems()
        //{
        //    if (this._selectedItems == null)
        //        return;

        //    this._selectedItems.Remove(this);
        //}
        #endregion
        #endregion // Presentation Members
    }
}