using FluentValidation;
using ICMServer.Models;
using ICMServer.Services.Data;
using ICMServer.WPF.ViewModels;
using System.IO;
using System.Linq;

namespace ICMServer.WPF.Validators
{
    class AdvertisementValidator : AbstractValidator<AdvertisementViewModel>
    {
        private const string titleProperty = "'標題'";
        private const string filePathProperty = "'檔案路徑'";
        private readonly IDataService<advertisement> dataService;

        private bool IsUniqueFilePath(AdvertisementViewModel data, string filePath)
        {
            bool result = true;
            var dataInDB = dataService.Select((d) => d.C_path == filePath).FirstOrDefault();
            if (dataInDB != null)
            {
                if (dataInDB.C_id != data.ID)
                {
                    result = false;
                }
            }
            return result;
        }
        
        private bool IsUniqueTitle(AdvertisementViewModel data, string title)
        {
            bool result = true;
            var dataInDB = dataService.Select((d) => d.C_title == title).FirstOrDefault();
            if (dataInDB != null)
            {
                if (dataInDB.C_id != data.ID)
                {
                    result = false;
                }
            }
            return result;
        }

        public AdvertisementValidator(IDataService<advertisement> dataService)
        {
            this.dataService = dataService;
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(advertisement => advertisement.Title)
                .NotEmpty()
                .WithMessage(titleProperty + "欄位不可是空白")
                .Length(1, 60)
                .WithMessage(titleProperty + "欄位字元個數必須在1~60個之間")
                .Must((advertisement, title) => IsUniqueTitle(advertisement, title))
                .WithMessage("已存在重複的" + titleProperty);

            RuleFor(advertisement => advertisement.FilePath)
                .NotEmpty()
                .WithMessage(filePathProperty + "欄位不可是空白")
                .Length(3, 255)
                .WithMessage(filePathProperty + "欄位字元個數必須在3~255個之間")
                .Must((advertisement, filePath) => IsUniqueFilePath(advertisement, filePath))
                .WithMessage("已存在重複的" + filePathProperty)
                .Must((filePath) => File.Exists(filePath))
                .WithMessage((filePath) => filePath + "不存在");
        }
    }
}
