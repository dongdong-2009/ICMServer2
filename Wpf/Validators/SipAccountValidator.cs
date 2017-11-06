using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.Linq;

namespace ICMServer.WPF.Validators
{
    class SipAccountValidator : AbstractValidator<SipAccountViewModel>
    {
        private const string nameProperty = "'用戶名'";
        private readonly IDataService<sipaccount> dataService;

        public SipAccountValidator(IDataService<sipaccount> dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(account => account.Name)
                .NotEmpty()
                .WithMessage(nameProperty + "不可是空白")
                .Matches(@"^[0-9]{12}$")
                .WithMessage(nameProperty + "欄位內容必須是12個數字")
                .Must((account, Name) => MatchesGroup(account, Name))
                .WithMessage((account) => nameProperty + "欄位內容開頭必須是" + account.Group)
                .Must((account, Name) => IsUniqueAccountName(account, Name))
                .WithMessage("已存在重複的" + nameProperty); ;
        }

        private bool MatchesGroup(SipAccountViewModel account, string name)
        {
            bool result = true;
            if (!string.IsNullOrWhiteSpace(account.Group))
                result = account.Name.StartsWith(account.Group);
            return result;
        }

        private bool IsUniqueAccountName(SipAccountViewModel account, string name)
        {
            bool result = true;
            sipaccount accountInDB = dataService.Select((c) => c.C_user == account.Name).FirstOrDefault();
            if (accountInDB != null)
            {
                result = false;
            }
            return result;
        }
    }
}
