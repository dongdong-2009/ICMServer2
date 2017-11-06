using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class ApplicationViewModel : ViewModelBase
    {
        public ApplicationViewModel()
        {
            // Add available pages
            
        }

        #region ChangePageCommand
        private ICommand _changePageCommand;
        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new RelayCommand<ViewModelBase>(
                    p => ChangeViewModel(p),
                    p => p is ViewModelBase));
            }
        }

        private void ChangeViewModel(ViewModelBase viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
        #endregion

        private List<ViewModelBase> _pageViewModels;
        public List<ViewModelBase> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<ViewModelBase>();

                return _pageViewModels;
            }
        }

        private ViewModelBase _currentPageViewModel;
        public ViewModelBase CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set { this.Set(ref _currentPageViewModel, value); }
        }
    }
}
