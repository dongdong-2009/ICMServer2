using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class UpgradeFileViewModel : SingleDataViewModelBase<upgrade, UpgradeFileViewModel>
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the UpgradeFileViewModel class.
        /// </summary>
        public UpgradeFileViewModel(
            IValidator<UpgradeFileViewModel> validator,
            ICollectionModel<upgrade> dataModel,
            IDialogService dialogService,
            upgrade data = null) : base(validator, dataModel, data)
        {
            this._dialogService = dialogService;
        }

#if DEBUG
        public UpgradeFileViewModel() : base()
        {
        }

        protected override void InitSampleData()
        {
            ID = 1;
            InputFilePath = Config.Instance.FTPServerRootDir + @"\" + @"data\firmware\201705031746363264_1.0_indoor\INDOOR.PKG";
            Version = "1.0";
            FileType = UpgradeFileType.SoftwareUpgrade;
            InsertedTime = DateTime.Now;
            IsDefault = true;
        }
#endif

        protected override void InitDefaultValue()
        {
            FileType = UpgradeFileType.SoftwareUpgrade;
            DeviceType = ICMServer.Models.DeviceType.Indoor_Phone;
        }

        protected override void ModelToViewModel()
        {
            ID = this._data.id;
            FilePath = !string.IsNullOrEmpty(this._data.filepath) ? Config.Instance.FTPServerRootDir + @"\" + this._data.filepath : null;
            Version = this._data.version;
            try { FileType = (UpgradeFileType?)this._data.filetype; } catch (Exception) { }
            try { DeviceType = (DeviceType?)this._data.device_type; } catch (Exception) { }
            InsertedTime = this._data.time;
            this.IsDefault = this._data.is_default.HasValue && this._data.is_default != 0;
            FileSize = this._data.FileSize;
        }

        protected override void ViewModelToModel()
        {
            this._data.filepath = FilePath.Substring((Config.Instance.FTPServerRootDir + @"\").Length);
            this._data.version = Version;
            this._data.filetype = (int?)FileType;
            this._data.device_type = (int?)DeviceType;
            this._data.time = InsertedTime ?? DateTime.Now;
            this._data.is_default = IsDefault ? 1 : 0;
            this._data.FileSize = FileSize;
        }

        #region Data Mapping Properties

        public int ID { get; set; }

        private string _InputFilePath;
        /// <summary>
        /// 文件保存位置
        /// </summary>
        public string InputFilePath
        {
            get { return _InputFilePath; }
            set
            {
                if (this.Set(ref _InputFilePath, value))
                {
                    try
                    {
                        FileSize = new System.IO.FileInfo(_InputFilePath).Length;
                        using (FileStream input = new FileStream(this._InputFilePath, FileMode.Open))
                        {
                            BinaryReader reader = new BinaryReader(input);
                            reader.ReadBytes(8);    // skip 8 bytes;
                            this.Version = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(8)).TrimEnd('\0');
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        protected string FilePath { get; set; }

        private string _Version;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return _Version; }
            protected set { this.Set(ref _Version, value); }
        }

        public UpgradeFileType? _FileType;
        /// <summary>
        /// 升级文件类型
        /// </summary>
        public UpgradeFileType? FileType
        {
            get { return _FileType; }
            set
            {
                if (this.Set(ref _FileType, value))
                {
                    this.RaisePropertyChanged(() => DeviceType);
                    this.RaisePropertyChanged(() => DefaultFileName);
                }
            }
        }

        private DeviceType? _DeviceType;
        /// <summary>
        /// 升级设备类型
        /// </summary>
        public DeviceType? DeviceType
        {
            get { return _FileType == UpgradeFileType.SoftwareUpgrade ? _DeviceType : null; }
            set
            {
                if (this.Set(ref _DeviceType, value))
                {
                    this.RaisePropertyChanged(() => DefaultFileName);
                }
            }
        }

        private DateTime? _InsertedTime;
        /// <summary>
        /// 上传时间
        /// </summary>
        public System.DateTime? InsertedTime
        {
            get { return _InsertedTime; }
            set { this.Set(ref _InsertedTime, value); }
        }

        private bool _IsDefault;
        /// <summary>
        /// 是否是默认升级文件
        /// </summary>
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { this.Set(ref _IsDefault, value); }
        }

        private long _fileSize;
        public long FileSize
        {
            get { return _fileSize; }
            protected set { this.Set(ref _fileSize, value); }
        }
        #endregion
               
        #region PickFileCommand
        private RelayCommand _PickFileCommand;
        public RelayCommand PickFileCommand
        {
            get
            {
                return _PickFileCommand ?? (_PickFileCommand = new RelayCommand(() =>
                {
                    //Messenger.Default.Send(OpenFileDialog
                    _dialogService.ShowOpenFileDialog(
                        "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*",
                        this.DefaultFileName,
                        (PickedFilePath) =>
                        {
                            this.InputFilePath = PickedFilePath;

                            //byte[] data = new byte[8];
                            //using (BinaryReader reader = new BinaryReader(new FileStream(InputFilePath, FileMode.Open)))
                            //{
                            //    reader.BaseStream.Seek(8, SeekOrigin.Begin);
                            //    reader.Read(data, 0, 8);
                            //    this.Version = System.Text.Encoding.ASCII.GetString(data).TrimEnd('\0');
                            //}
                        });
                }));
            }
        }
        #endregion

        public string DefaultFileName
        {
            get
            {
                string fileName = "";
                switch (FileType)
                {
                    case UpgradeFileType.SoftwareUpgrade:
                        switch (DeviceType)
                        {
                            case ICMServer.Models.DeviceType.Door_Camera:
                            case ICMServer.Models.DeviceType.Emergency_Intercom_Unit:
                                fileName = "OUTDOOR.PKG";
                                break;

                            case ICMServer.Models.DeviceType.Lobby_Phone_Unit:
                            case ICMServer.Models.DeviceType.Lobby_Phone_Building:
                            case ICMServer.Models.DeviceType.Lobby_Phone_Area:
                                fileName = "LOBBY.PKG";
                                break;

                            case ICMServer.Models.DeviceType.Indoor_Phone:
                            case ICMServer.Models.DeviceType.Indoor_Phone_SD:
                                fileName = "INDOOR.PKG";
                                break;

                            case ICMServer.Models.DeviceType.Administrator_Unit:
                                fileName = "ADMIN.PKG";
                                break;
                        }
                        break;

                    case UpgradeFileType.AddressBookUpgrade:
                        fileName = "ADDRESS.PKG";
                        break;

                    case UpgradeFileType.ScreenSaverUpgrade:
                        fileName = "SCREENSAVER.PKG";
                        break;

                    case UpgradeFileType.SecurityCardListUpgrade:
                        fileName = "CARD.PKG";
                        break;
                }
                return fileName;
            }
        }

        private readonly string[] DevicePkgFileNames =
        {
            "OUTDOOR.PKG",
            "LOBBY.PKG",
            "LOBBY.PKG",
            "LOBBY.PKG",
            "INDOOR.PKG",
            "INDOOR.PKG",
            "ADMIN.PKG",
            "OUTDOOR.PKG",
        };

        private readonly ICMServer.Models.DeviceType[] _UpgradableDeviceTypes =
        {
             ICMServer.Models.DeviceType.Lobby_Phone_Unit,
             ICMServer.Models.DeviceType.Lobby_Phone_Building,
             ICMServer.Models.DeviceType.Lobby_Phone_Area,
             ICMServer.Models.DeviceType.Indoor_Phone,
             ICMServer.Models.DeviceType.Administrator_Unit
        };

        public ICMServer.Models.DeviceType[] UpgradableDeviceTypes
        { 
            get { return _UpgradableDeviceTypes; } 
        }

        protected override async Task AddAsync()
        {
            this._data = new upgrade();
            if (this._dataModel != null && this._data != null)
            {
                DateTime now = DateTime.Now;
                this.InsertedTime = now;
                FilePath = string.Format(@"{0}\{1}_{2}_{3}\{4}",
                    Path.GetUpgradeDataBaseFolderPath(), 
                    now.ToString("yyyyMMddHHmmssffff"), 
                    this.Version, 
                    this.TypeName,
                    this.DefaultFileName);

                if (!Directory.Exists(System.IO.Path.GetDirectoryName(FilePath)))
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(FilePath));
                File.Copy(InputFilePath, FilePath);

                ViewModelToModel();
                await this._dataModel.InsertAsync(this._data);
            }
        }

        protected string TypeName
        {
            get
            {
                string typeName = "";
                switch (FileType)
                {
                    case UpgradeFileType.SoftwareUpgrade:
                        switch (DeviceType)
                        {
                            case ICMServer.Models.DeviceType.Door_Camera:
                                typeName = "doorcamera";
                                break;

                            case ICMServer.Models.DeviceType.Lobby_Phone_Unit:
                                typeName = "lobyyu";
                                break;

                            case ICMServer.Models.DeviceType.Lobby_Phone_Building:
                                typeName = "lobyyb";
                                break;

                            case ICMServer.Models.DeviceType.Lobby_Phone_Area:
                                typeName = "lobyya";
                                break;

                            case ICMServer.Models.DeviceType.Indoor_Phone:
                            case ICMServer.Models.DeviceType.Indoor_Phone_SD:
                                typeName = "indoor";
                                break;

                            case ICMServer.Models.DeviceType.Administrator_Unit:
                                typeName = "manager";
                                break;

                            case ICMServer.Models.DeviceType.Emergency_Intercom_Unit:
                                typeName = "public";
                                break;
                        }
                        break;

                    case UpgradeFileType.AddressBookUpgrade:
                        typeName = "address";
                        break;

                    case UpgradeFileType.ScreenSaverUpgrade:
                        typeName = "screen";
                        break;

                    case UpgradeFileType.SecurityCardListUpgrade:
                        typeName = "card";
                        break;
                }
                return typeName;
            }
        }
    }
}
