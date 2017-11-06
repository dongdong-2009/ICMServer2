using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICMServer.WPF.Validators
{
    class DeviceValidator : AbstractValidator<DeviceViewModel>
    {
        private const string ipAddressProperty = "'IP'";
        private const string gatewayProperty = "'Gateway'";
        private const string subnetMaskProperty = "'SubnetMask'";
        private const string deviceAddressProperty = "'設備地址'";
        private const string ipCamIDProperty = "'IP Cam 帳號'";
        private const string ipCamPasswordProperty = "'IP Cam 密碼'";
        private readonly IDeviceDataService dataService;

        private bool IsValidIPAddress(string ip)
        {
            return Regex.Match(ip, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$").Length > 0;
        }

        /// <summary>
        /// IP地址的正则表达式
        /// </summary>
        public static readonly Regex IpRegex = new
            Regex(@"^((2[0-4]\d|25[0-5]|(1\d{2})|([1-9]?[0-9]))\.){3}(2[0-4]\d|25[0-4]|(1\d{2})|([1-9][0-9])|([1-9]))$");
        
        private bool IsUniqueIPAddress(DeviceViewModel device, string ip)
        {
            bool result = true;
            //DebugLog.TraceMessage(string.Format("ip {0}", ip));
            Device deviceInDB = dataService.Select((d) => d.ip == ip).FirstOrDefault();
            if (deviceInDB != null)
            {
                if (deviceInDB.id != device.ID)
                {
                    result = false;
                }
            }
            //DebugLog.TraceMessage(string.Format("ip {0} {1}", ip, result));
            return result;
        }

        private bool IsUniqueDeviceAddress(DeviceViewModel device, string deviceAddress)
        {
            bool result = true;
            Device deviceInDB = dataService.Select((d) => d.roomid == deviceAddress).FirstOrDefault();
            if (deviceInDB != null)
            {
                if (deviceInDB.id != device.ID)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool IsValidDeviceAddress(string deviceAddress)
        {
            return Regex.Match(deviceAddress, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$").Length > 0;
        }

        private bool IsUniqueControlServer(DeviceViewModel device)
        {
            bool result = true;
            Device deviceInDB = dataService.Select((d) => d.type == (int)DeviceType.Control_Server).FirstOrDefault();
            if (deviceInDB != null)
            {
                if (deviceInDB.id != device.ID)
                {
                    result = false;
                }
            }
            return result;
        }

        public DeviceValidator(IDeviceDataService dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(device => device.IPAddress)
                .NotEmpty()
                .WithMessage(ipAddressProperty + "欄位不可是空白")
                .Must(ip => IpRegex.IsMatch(ip))
                .WithMessage(ipAddressProperty + "欄位格式不正確")
                .Must((device, ip) => IsUniqueIPAddress(device, ip))
                .WithMessage("已存在重複的" + ipAddressProperty);

            RuleFor(device => device.Gateway)
                .NotEmpty()
                .WithMessage(gatewayProperty + "欄位不可是空白")
                .Must(ip => IpRegex.IsMatch(ip))
                .WithMessage(gatewayProperty + "欄位格式不正確");

            RuleFor(device => device.SubnetMask)
                .NotEmpty()
                .WithMessage(subnetMaskProperty + "欄位不可是空白")
                .Must(ip => IsValidIPAddress(ip))
                .WithMessage(subnetMaskProperty + "欄位格式不正確");

            RuleFor(device => device.DeviceAddress)
                .NotEmpty()
                .WithMessage(deviceAddressProperty + "欄位不可是空白")
                .Must(deviceAddress => IsValidDeviceAddress(deviceAddress))
                .WithMessage(deviceAddressProperty + "欄位格式不正確")
                .Must((device, deviceAddress) => IsUniqueDeviceAddress(device, deviceAddress))
                .WithMessage("已存在重複的" + deviceAddressProperty);

            RuleFor(device => device.IPCamID)
                .NotEmpty()
                .WithMessage(ipCamIDProperty + "欄位不可是空白")
                .When(device => device.DeviceType == (int)DeviceType.IPCAM);

            RuleFor(device => device.IPCamPassword)
                .NotEmpty()
                .WithMessage(ipCamPasswordProperty + "欄位不可是空白")
                .When(device => device.DeviceType == (int)DeviceType.IPCAM);

            RuleFor(device => device.DeviceType)
                .Must((device, deviceType) => IsUniqueControlServer(device))
                .WithMessage("控制伺服器只能有一台")
                .When(device => device.DeviceType == (int)DeviceType.Control_Server);
        }
    }
}
