using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;

namespace ICMServer.WPF.ViewModels
{
    public class UserViewModel : SingleDataViewModelBase<user, UserViewModel>
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the AdvertisementViewModel class.
        /// </summary>
        public UserViewModel(
            IValidator<UserViewModel> validator,
            ICollectionModel<user> dataModel,
            IDialogService dialogService,
            user data = null) : base(validator, dataModel, data)
        {
            this._dialogService = dialogService;
        }

#if DEBUG
        public UserViewModel() : base()
        {
        }

        protected override void InitSampleData()
        {
            //ID = 1;
            //InputFilePath = Config.Instance.FtpServerRootDir + @"\" + @"data\firmware\201705031746363264_1.0_indoor\INDOOR.PKG";
            //Version = "1.0";
            //FileType = UpgradeFileType.SoftwareUpgrade;
            //InsertedTime = DateTime.Now;
            //IsDefault = true;
        }
#endif

        protected override void InitDefaultValue()
        {
            //FileType = UpgradeFileType.SoftwareUpgrade;
            //DeviceType = ICMServer.Models.DeviceType.Indoor_Phone;
        }

        protected override void ModelToViewModel()
        {
            //ID = this._data.id;
            //FilePath = !string.IsNullOrEmpty(this._data.filepath) ? Config.Instance.FtpServerRootDir + @"\" + this._data.filepath : null;
            //Version = this._data.version;
            //try { FileType = (UpgradeFileType?)this._data.filetype; } catch (Exception) { }
            //try { DeviceType = (DeviceType?)this._data.device_type; } catch (Exception) { }
            //InsertedTime = this._data.time;
            //this.IsDefault = this._data.is_default.HasValue && this._data.is_default != 0;
        }

        protected override void ViewModelToModel()
        {
            //this._data.filepath = FilePath.Substring((Config.Instance.FtpServerRootDir + @"\").Length);
            //this._data.version = Version;
            //this._data.filetype = (int?)FileType;
            //this._data.device_type = (int?)DeviceType;
            //this._data.time = InsertedTime ?? DateTime.Now;
            //this._data.is_default = IsDefault ? 1 : 0;
        }

        #region Data Mapping Properties

        public int ID { get; set; }

        // 用户序列号
        public string C_userno { get; set; }
        // 用户名称
        public string C_username { get; set; }

        // 用户权限(为authority表中的_id key)
        //public Nullable<int> C_powerid { get; set; }

        private string _Password;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string C_password
        {
            get { return _Password; }
            set { this.Set(ref _Password, value); }
        }
        #endregion      
    }
}
