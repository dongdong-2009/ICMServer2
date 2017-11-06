using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;

namespace ICMServer.WPF.ViewModels
{
    public class AdvertisementViewModel : SingleDataViewModelBase<advertisement, AdvertisementViewModel>
    {
        IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the AdvertisementViewModel class.
        /// </summary>
        public AdvertisementViewModel(
            IValidator<AdvertisementViewModel> validator,
            ICollectionModel<advertisement> dataModel,
            IDialogService dialogService,
            advertisement data = null) : base(validator, dataModel, data)
        {
            this._dialogService = dialogService;
        }

#if DEBUG
        public AdvertisementViewModel() : base() { }

        protected override void InitSampleData()
        {
            ID = 1;
            PlayOrder = 1;
            Title = "玩具總動員";
            InsertedTime = DateTime.Now;
            FilePath = @"D:\玩具總動員.mp4";
        }
#endif

        protected override void InitDefaultValue()
        {
        }

        protected override void ModelToViewModel()
        {
            ID = this._data.C_id;
            PlayOrder = this._data.C_no;
            Title = this._data.C_title;
            InsertedTime = this._data.C_time;
            FilePath = this._data.C_path;
        }

        protected override void ViewModelToModel()
        {
            this._data.C_no = PlayOrder;
            this._data.C_title = Title;
            this._data.C_time = InsertedTime;
            this._data.C_path = FilePath;
        }

        private RelayCommand _pickFileCommand;
        public RelayCommand PickFileCommand
        {
            get
            {
                return _pickFileCommand ?? (_pickFileCommand = new RelayCommand(() =>
                {
                    const string videoFileExtsList = "*.3gp;*.avi;*.ts;*.mkv*.mov;*.mp4;*.mpg;*.mpeg";

                    _dialogService.ShowOpenFileDialog(
                         string.Format("Video files ({0})|{0}|All files (*.*)|*.*", videoFileExtsList),
                         "",
                         (PickedFilePath) => { this.FilePath = PickedFilePath; });
                },
                () => { return true; }));
            }
        }


        //private ShowOpenFileDialogMessage _OpenFileDialog;
        //private ShowOpenFileDialogMessage OpenFileDialog
        //{
        //    get
        //    {
        //        const string videoFileExtsList = "*.3gp;*.avi;*.ts;*.mkv*.mov;*.mp4;*.mpg;*.mpeg";

        //        return _OpenFileDialog ?? (_OpenFileDialog = new ShowOpenFileDialogMessage
        //        {
        //            Filter = string.Format("Video files ({0})|{0}|All files (*.*)|*.*", videoFileExtsList),
        //            FilePicked = () =>
        //            {
        //                this.FilePath = _OpenFileDialog.PickedFilePath;
        //            }
        //        });
        //    }
        //}

        #region Data Mapping Properties

        public int ID { get; set; }

        private int? _playOrder;
        /// <summary>
        /// 播放順序
        /// </summary>
        public int? PlayOrder
        {
            get { return _playOrder; }
            set { this.Set(ref _playOrder, value); }
        }

        private string _title;
        /// <summary>
        /// 影片標題
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { this.Set(ref _title, value); }
        }

        private DateTime? _insertedTime;
        /// <summary>
        /// 影片上傳時間
        /// </summary>
        public DateTime? InsertedTime
        {
            get { return _insertedTime; }
            set { this.Set(ref _insertedTime, value); }
        }

        private string _filePath;
        /// <summary>
        /// 影片位於icm server內的絕對路徑
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { this.Set(ref _filePath, value); }
        }
        #endregion
    }
}
