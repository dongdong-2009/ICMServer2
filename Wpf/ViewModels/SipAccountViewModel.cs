using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;

namespace ICMServer.WPF.ViewModels
{
    public class SipAccountViewModel : SingleDataViewModelBase<sipaccount, SipAccountViewModel>
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the SipAccountViewModel class.
        /// </summary>
        public SipAccountViewModel(
            IValidator<SipAccountViewModel> validator,
            ICollectionModel<sipaccount> dataModel,
            IDialogService dialogService,
            sipaccount data = null) : base(validator, dataModel, data)
        {
            this._dialogService = dialogService;
        }

#if DEBUG
        public SipAccountViewModel() : base()
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
        }

        protected override void ModelToViewModel()
        {
            this.Name = this._data.C_user;
            this.RoomAddress = this._data.C_room;
            this.Group = this._data.C_usergroup;
        }

        protected override void ViewModelToModel()
        {
            this._data.C_user = this.Name;
            this._data.C_room = this.RoomAddress;
            this._data.C_usergroup = this.Group;
        }

        #region Data Mapping Properties
        private string _Name;
        /// <summary>
        /// 用戶名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { this.Set(ref _Name, value); }
        }

        private string _RoomAddress;
        /// <summary>
        /// 房間號
        /// </summary>
        public string RoomAddress
        {
            get { return _RoomAddress; }
            set
            {
                if (this.Set(ref _RoomAddress, value))
                {
                    if (string.IsNullOrWhiteSpace(_RoomAddress))
                    {
                        this.Group = "";
                    }
                    else
                    {
                        this.Group = _RoomAddress.Replace("-", "");
                        if (string.IsNullOrWhiteSpace(this.Name))
                            this.Name = this.Group + "00";
                    }
                }
            }
        }

        private string _Group;
        /// <summary>
        /// 用戶組
        /// </summary>
        public string Group
        {
            get { return _Group; }
            private set { this.Set(ref _Group, value); }
        }
        #endregion
    }
}
