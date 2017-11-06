using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICMServer.WPF.Validators
{
    class DialogInputIPAddressValidator : AbstractValidator<DialogInputIPAddressViewModel>
    {
        private const string ipAddressProperty = "'IP'";
        private readonly IDeviceDataService dataService;

        /// <summary>
        /// IP地址的正则表达式
        /// </summary>
        public static readonly Regex IpRegex = new
            Regex(@"^((2[0-4]\d|25[0-5]|(1\d{2})|([1-9]?[0-9]))\.){3}(2[0-4]\d|25[0-4]|(1\d{2})|([1-9][0-9])|([1-9]))$");

        private bool IPAddressExists(string ip)
        {
            bool result = false;
            //DebugLog.TraceMessage(string.Format("ip {0}", ip));
            Device device = dataService.Select((d) => d.ip == ip).FirstOrDefault();
            if (device != null)
            {
                result = true;
            }
            //DebugLog.TraceMessage(string.Format("ip {0} {1}", ip, result));
            return result;
        }

        public DialogInputIPAddressValidator(IDeviceDataService dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(input => input.IPAddress)
                .NotEmpty()
                .WithMessage(ipAddressProperty + "欄位不可是空白")
                .Must(ip => IpRegex.IsMatch(ip))
                .WithMessage(ipAddressProperty + "欄位格式不正確")
                .Must(ip => IPAddressExists(ip))
                .WithMessage("不存在此" + ipAddressProperty);

        }
    }
}
