using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICMServer.WPF.Validators
{
    class SecurityCardValidator : AbstractValidator<SecurityCardViewModel>
    {
        private const string cardNumberProperty = "'卡號'";
        private const string roomAddressProperty = "'房號'";
        private readonly IDataService<iccard> dataService;

        public SecurityCardValidator(IDataService<iccard> dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(card => card.CardNumber)
                .NotEmpty()
                .WithMessage(cardNumberProperty + "不可是空白")
                .Matches(@"^[a-zA-Z0-9]{16}$")
                .WithMessage(cardNumberProperty + "欄位內容必須是16個英文或數字")
                .Must((card, CardNumber) => IsUniqueCardNumber(card, CardNumber))
                .WithMessage("已存在重複的" + cardNumberProperty); ;

            RuleFor(card => card.RoomAddress)
                .NotEmpty()
                .WithMessage(cardNumberProperty + "不可是空白")
                .Must(roomAddress => IsValidRoomAddress(roomAddress))
                .WithMessage(roomAddressProperty + "欄位格式不正確");
        }

        private bool IsUniqueCardNumber(SecurityCardViewModel card, string cardNumber)
        {
            bool result = true;
            iccard cardInDB = dataService.Select((c) => c.C_icno == card.CardNumber).FirstOrDefault();
            if (cardInDB != null)
            {
                if (cardInDB.C_icid != card.ID)
                {
                    result = false;
                }
            }
            return result;
        }

        private bool IsValidRoomAddress(string deviceAddress)
        {
            return Regex.Match(deviceAddress, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$").Length > 0;
        }
    }
}
