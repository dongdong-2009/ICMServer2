using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;

namespace ICMServer.WPF.Validators
{
    class AnnouncementValidator : AbstractValidator<AnnouncementViewModel>
    {
        private const string titleProperty = "'標題'";
        private const string roomAddressProperty = "'發送地址'";
        private const string imageFilePathProperty = "'圖片位址'";
        private const string textContentProperty = "'文字內容'";
        private readonly IDataService<Announcement> dataService;

        public AnnouncementValidator(IDataService<Announcement> dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(announcement => announcement.Title)
                .NotEmpty()
                .WithMessage(titleProperty + "欄位不可是空白")
                .Length(1, 50)
                .WithMessage(titleProperty + "欄位字元個數必須在1~60個之間");

            //RuleFor(announcement => announcement.DstRoomAddress)
            //    .NotEmpty()
            //    .WithMessage(roomAddressProperty + "欄位不可是空白");

            RuleFor(announcement => announcement.ImageFilePath)
                .NotEmpty()
                .WithMessage(imageFilePathProperty + "不可是空白")
                .When(announcement => announcement.Type == MessageType.Image);

            RuleFor(announcement => announcement.TextContent)
                .NotEmpty()
                .WithMessage(textContentProperty + "不可是空白")
                .When(announcement => announcement.Type == MessageType.Text);
        }
    }
}
