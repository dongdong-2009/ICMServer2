using FluentValidation;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.Text.RegularExpressions;

namespace ICMServer.WPF.Validators
{
    class ResidentValidator : AbstractValidator<ResidentViewModel>
    {
        private const string nameProperty = "'姓名'";
        private readonly IDeviceDataService dataService;

        private bool IsValidDeviceAddress(string deviceAddress)
        {
            return Regex.Match(deviceAddress, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$").Length > 0;
        }

        public ResidentValidator(IDeviceDataService dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(device => device.Name)
                .NotEmpty()
                .WithMessage(nameProperty + "欄位不可是空白")
                .Length(2, 50)
                .WithMessage(nameProperty + "欄位字元個數必須在2~50個之間");
                        
            //RuleFor(device => device.DeviceAddress)
            //    .NotEmpty()
            //    .WithMessage(deviceAddressProperty + "欄位不可是空白")
            //    .Must(deviceAddress => IsValidDeviceAddress(deviceAddress))
            //    .WithMessage(deviceAddressProperty + "欄位格式不正確")
            //    .Must((device, deviceAddress) => IsUniqueDeviceAddress(device, deviceAddress))
            //    .WithMessage("已存在重複的" + deviceAddressProperty);
        }
    }
}
