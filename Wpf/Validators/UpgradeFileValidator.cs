using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;

namespace ICMServer.WPF.Validators
{
    class UpgradeFileValidator : AbstractValidator<UpgradeFileViewModel>
    {
        private const string inputFilePathProperty = "'來源檔案路徑'";
        private readonly IDataService<upgrade> dataService;

        public UpgradeFileValidator(IDataService<upgrade> dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(info => info.InputFilePath)
                .NotEmpty()
                .WithMessage(inputFilePathProperty + "不可是空白");
        }
    }
}
