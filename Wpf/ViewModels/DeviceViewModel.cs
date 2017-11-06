using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DeviceViewModel : SingleDataViewModelBase<Device, DeviceViewModel>
    {
#if DEBUG
        public DeviceViewModel() : base() { }

        protected override void InitSampleData()
        {
            ID = 1;
            IPAddress = "192.168.100.100";
            DeviceAddress = "88-88-88-88-88-88";
            Alias = "範例裝置";
            MulticastGroupIP = "239.255.42.120";
            MacAddress = "112233445566";
            DeviceType = 10;
            SubnetMask = "255.255.255.0";
            Gateway = "192.168.100.1";
            IPCamID = "admin";
            IPCamPassword = "123456";
            IsOnline = true;
            AddressBookVersion = "1.0";
            CardListVersion = "1.0";
            FirmwareVersion = "1.0";
            LatestAddressBookVersion = "1.0";
            LatestCardListVersion = "1.1";
            LatestFirmwareVersion = "1.0";
        }
#endif

        private readonly IRoomsModel _roomDataModel;

        /// <summary>
        /// Initializes a new instance of the DeviceViewModel class.
        /// </summary>
        public DeviceViewModel(
            IValidator<DeviceViewModel> validator,
            ICollectionModel<Device> dataModel,
            IRoomsModel roomDataModel,
            Device data = null) : base(validator, dataModel, data)
        {
            this._roomDataModel = roomDataModel;
        }

        protected override void InitDefaultValue()
        {
            DeviceType = (int)ICMServer.Models.DeviceType.Indoor_Phone;
        }

        private string _oldRoomID = null;
        protected override void ModelToViewModel()
        {
            ID = this._data.id;
            IPAddress = this._data.ip;
            DeviceAddress = this._data.roomid;
            _oldRoomID = this._data.roomid.Substring(0, 14);
            Alias = this._data.alias;
            MulticastGroupIP = this._data.group;
            MacAddress = this._data.mac;
            DeviceType = this._data.type ?? -1;
            SubnetMask = this._data.sm;
            Gateway = this._data.gw;
            IPCamID = this._data.cameraid;
            IPCamPassword = this._data.camerapw;
            IsOnline = this._data.online != 0;          //
            AddressBookVersion = this._data.aVer;       //
            CardListVersion = this._data.cVer;          //
            FirmwareVersion = this._data.fVer;          //
            LatestAddressBookVersion = this._data.laVer;//
            LatestCardListVersion = this._data.lcVer;   //
            LatestFirmwareVersion = this._data.lfVer;   //
        }

        protected override void ViewModelToModel()
        {
            this._data.ip = IPAddress;
            this._data.roomid = DeviceAddress;
            this._data.alias = Alias;
            this._data.group = MulticastGroupIP;
            this._data.mac = MacAddress;
            this._data.type = DeviceType;
            this._data.sm = SubnetMask;
            this._data.gw = Gateway;
            this._data.cameraid = IPCamID;
            this._data.camerapw = IPCamPassword;
        }

        #region Data Mapping Properties
        public int ID { get; protected set; }

        // 裝置的IP address
        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set { this.Set(ref _ipAddress, value); }
        }

        // "ro", "00-00-00-00-00-01"
        private string _deviceAddress;
        public string DeviceAddress
        {
            get { return _deviceAddress; }
            set { this.Set(ref _deviceAddress, value); }
        }

        // 裝置別名，譬如"Control Server"，由使用者自行填寫
        private string _alias;
        public string Alias
        {
            get { return _alias; }
            set { this.Set(ref _alias, value); }
        }

        // multicast時的group ip address
        private string _multicastGroupIP;
        public string MulticastGroupIP
        {
            get { return _multicastGroupIP; }
            set { this.Set(ref _multicastGroupIP, value); }
        }

        // 裝置的mac address
        private string _macAddress;
        public string MacAddress
        {
            get { return _macAddress; }
            set { this.Set(ref _macAddress, value); }
        }

        private bool _isOnline;
        public bool IsOnline
        {
            get { return _isOnline; }
            private set { this.Set(ref _isOnline, value); }
        }

        // 裝置種類，0->Control Server
        private int _deviceType;
        public int DeviceType
        {
            get { return _deviceType; }
            set { this.Set(ref _deviceType, value); }
        }

        // 裝置的subnet mask
        private string _subnetMask;
        public string SubnetMask
        {
            get { return _subnetMask; }
            set { this.Set(ref _subnetMask, value); }
        }

        // 裝置的gateway ip address
        private string _gateway;
        public string Gateway
        {
            get { return _gateway; }
            set { this.Set(ref _gateway, value); }
        }

        // 裝置如果關連到某ip cam，登入該ip cam所需的帳號和密碼
        private string _ipCamID;
        public string IPCamID
        {
            get { return _ipCamID; }
            set { this.Set(ref _ipCamID, value); }
        }

        private string _ipCamPassword;
        public string IPCamPassword
        {
            get { return _ipCamPassword; }
            set { this.Set(ref _ipCamPassword, value); }
        }

        // 裝置地址薄版本
        public string AddressBookVersion { get; protected set; }

        // 裝置卡列表版本
        public string CardListVersion { get; protected set; }

        // 裝置軟件版本
        public string FirmwareVersion { get; protected set; }

        /// <summary>
        /// 裝置在服務器最新地址薄版本
        /// </summary>
        public string LatestAddressBookVersion { get; protected set; }

        // 裝置在服務器最新卡列表版本
        public string LatestCardListVersion { get; protected set; }

        // 裝置在服務器最新軟件版本
        public string LatestFirmwareVersion { get; protected set; }
        #endregion


        protected override async Task UpdateAsync()
        {
            if (this._dataModel != null && this._data != null)
            {
                ViewModelToModel();
                await this._dataModel.UpdateAsync(this._data);

                string roomID = this._data.roomid.Substring(0, 14);
                if (_oldRoomID != roomID)
                {
                    //var room = await Task.Run(() => this._roomDataModel.Select(r => r.ID == _oldRoomID).FirstOrDefault());
                    //if (room != null)
                    //{
                    //    room.ID = roomID;
                    //    await this._roomDataModel.UpdateAsync(room);
                    //}
                    Room room = new Room { ID = roomID };
                    await this._roomDataModel.InsertAsync(room);
                    await this._roomDataModel.DeleteRoomsWhichHaveNoDevicesAsync();
                }
            }
        }

        protected override async Task AddAsync()
        {
            this._data = new Device();
            if (this._dataModel != null && this._data != null)
            {
                ViewModelToModel();
                await this._dataModel.InsertAsync(this._data);

                Room room = new Room { ID = this._data.roomid.Substring(0, 14) };
                await this._roomDataModel.InsertAsync(room);
            }
        }

    }
}