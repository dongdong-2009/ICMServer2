using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.Validators;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogConfigSystemSettingViewModel : ValidatableViewModelBase
    {
        private readonly ICollectionModel<fs_province> _ProvinceDataModel;
        private readonly ICollectionModel<fs_city> _CityDataModel;

        public DialogConfigSystemSettingViewModel(
            IValidator<DialogConfigSystemSettingViewModel> validator,
            ICollectionModel<fs_province> provinceDataModel,
            ICollectionModel<fs_city> cityDataModel)
        {
            this._ProvinceDataModel = provinceDataModel;
            this._CityDataModel = cityDataModel;

            Cities = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_CityDataModel.Data);
            Provinces = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_ProvinceDataModel.Data);
            Provinces.CurrentChanged += Provinces_CurrentChanged;

            RefreshCommand.Execute(null);

            this.Validator = new FluentValidationAdapter<DialogConfigSystemSettingViewModel>(validator);

            this.AppName = Config.Instance.AppName;
            this.LocalIP = Config.Instance.LocalIP;
            this.LocalSubnetMask = Config.Instance.LocalSubnetMask;
            this.LocalGateway = Config.Instance.LocalGateway;
            this.OutboundIP = Config.Instance.OutboundIP;
            this.HTTPServerPort = Config.Instance.HTTPServerPort;
            this.NTPServerPort = 49123;
            this.SIPServerAddress = Config.Instance.SIPServerIP;
            this.SIPServerPort = Config.Instance.SIPServerPort;
            this.SIPCommunicationPort = Config.Instance.SIPCommunicationPort;
            this.CloudSolution = Config.Instance.CloudSolution;
            this.ProvinceID = Config.Instance.WeatherOfProvince;
            this.CityID = Config.Instance.WeatherOfCity;
            this.ProvinceID = Config.Instance.WeatherOfProvince;
        }

        private void Provinces_CurrentChanged(object sender, EventArgs e)
        {
            this.Cities.Filter = new Predicate<object>(FilterByProvinceID);
            //throw new NotImplementedException();
        }

        private bool FilterByProvinceID(object obj)
        {
            fs_city city = obj as fs_city;
            if (city != null && city.ProvinceID == this.ProvinceID)
                return true;
            return false;
        }

        private string _AppName;
        public string AppName
        {
            get { return _AppName; }
            set { this.Set(ref _AppName, value); }
        }

        private string _LocalIP;
        public string LocalIP
        {
            get { return _LocalIP; }
            set { this.Set(ref _LocalIP, value); }
        }

        private string _LocalSubnetMask;
        public string LocalSubnetMask
        {
            get { return _LocalSubnetMask; }
            set { this.Set(ref _LocalSubnetMask, value); }
        }

        private string _LocalGateway;
        public string LocalGateway
        {
            get { return _LocalGateway; }
            set { this.Set(ref _LocalGateway, value); }
        }

        private string _OutboundIP;
        public string OutboundIP
        {
            get { return _OutboundIP; }
            set { this.Set(ref _OutboundIP, value); }
        }

        private int _HTTPServerPort;
        public int HTTPServerPort
        {
            get { return _HTTPServerPort; }
            private set { this.Set(ref _HTTPServerPort, value); }
        }

        private int _NTPServerPort;
        public int NTPServerPort
        {
            get { return _NTPServerPort; }
            private set { this.Set(ref _NTPServerPort, value); }
        }

        private string _SIPServerAddress;
        public string SIPServerAddress
        {
            get { return _SIPServerAddress; }
            set { this.Set(ref _SIPServerAddress, value); }
        }

        private int _SIPServerPort;
        public int SIPServerPort
        {
            get { return _SIPServerPort; }
            set { this.Set(ref _SIPServerPort, value); }
        }

        private int _SIPCommunicationPort;
        public int SIPCommunicationPort
        {
            get { return _SIPCommunicationPort; }
            set { this.Set(ref _SIPCommunicationPort, value); }
        }

        private CloudSolution _CloudSolution;
        public CloudSolution CloudSolution
        {
            get { return _CloudSolution; }
            set { this.Set(ref _CloudSolution, value); }
        }

        #region Provinces
        private ListCollectionView _Provinces;
        public ListCollectionView Provinces
        {
            get { return _Provinces; }
            private set { this.Set(ref _Provinces, value); }
        }
        #endregion

        #region Cities
        private ListCollectionView _Cities;
        public ListCollectionView Cities
        {
            get { return _Cities; }
            private set { this.Set(ref _Cities, value); }
        }
        #endregion

        private long _ProvinceID;
        public long ProvinceID
        {
            get { return _ProvinceID; }
            set { this.Set(ref _ProvinceID, value); }
        }

        private long _CityID;
        public long CityID
        {
            get { return _CityID; }
            set { this.Set(ref _CityID, value); }
        }

        #region SaveCommand
        private IAsyncCommand _SaveCommand;
        public IAsyncCommand SaveCommand
        {
            get { return _SaveCommand ?? (_SaveCommand = AsyncCommand.Create(
                () => { return Task.Run(() => { DoSave(); }); },
                () => { return this.HasErrors == false; })); }
        }

        private void DoSave()
        {
            if (Config.Instance.AppName != this.AppName)
                Config.Instance.AppName = this.AppName;
            if (Config.Instance.LocalIP != this.LocalIP)
                Config.Instance.LocalIP = this.LocalIP;
            if (Config.Instance.LocalSubnetMask != this.LocalSubnetMask)
                Config.Instance.LocalSubnetMask = this.LocalSubnetMask;
            if (Config.Instance.LocalGateway != this.LocalGateway)
                Config.Instance.LocalGateway = this.LocalGateway;
            if (Config.Instance.OutboundIP != this.OutboundIP)
                Config.Instance.OutboundIP = this.OutboundIP;
            if (Config.Instance.HTTPServerPort != this.HTTPServerPort)
                Config.Instance.HTTPServerPort = this.HTTPServerPort;
            if (Config.Instance.SIPServerIP != this.SIPServerAddress)
                Config.Instance.SIPServerIP = this.SIPServerAddress;
            if (Config.Instance.SIPServerPort != this.SIPServerPort)
                Config.Instance.SIPServerPort = this.SIPServerPort;
            if (Config.Instance.SIPCommunicationPort != this.SIPCommunicationPort)
                Config.Instance.SIPCommunicationPort = this.SIPCommunicationPort;
            if (Config.Instance.CloudSolution != this.CloudSolution)
                Config.Instance.CloudSolution = this.CloudSolution;
            if (Config.Instance.WeatherOfProvince != this.ProvinceID)
                Config.Instance.WeatherOfProvince = (int)this.ProvinceID;
            if (Config.Instance.WeatherOfCity != this.CityID)
                Config.Instance.WeatherOfCity = (int)this.CityID;
        }
        #endregion

        #region RefreshCommand
        private ICommand _refreshCommand;
        private ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(
                    async () => 
                    {
                        await _ProvinceDataModel.RefillDataAsync();
                        await _CityDataModel.RefillDataAsync();
                    }));
            }
        }
        #endregion
    }
}