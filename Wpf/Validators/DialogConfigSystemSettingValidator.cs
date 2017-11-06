using FluentValidation;
using ICMServer.WPF.ViewModels;

namespace ICMServer.WPF.Validators
{
    public class DialogConfigSystemSettingValidator : AbstractValidator<DialogConfigSystemSettingViewModel>
    {
        public DialogConfigSystemSettingValidator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;  // 宣告一個 RuleFor 有多個檢查條件時，只要檢查到第一個錯就停止往下檢查。

            RuleFor(viewModel => viewModel.SIPCommunicationPort)
                .NotEqual(5060)
                .WithMessage("當雲對講服務採用PPHook解決方案時，SIP語音通訊埠設定值不可是5060")
                .When(viewModel => viewModel.CloudSolution == CloudSolution.PPHook);

            RuleFor(viewModel => viewModel.SIPServerAddress)
                .NotEmpty()
                .WithMessage("當雲對講服務採用SIP Server解決方案時，SIP伺服器地址不可為空")
                .When(viewModel => viewModel.CloudSolution == CloudSolution.SIPServer);
        }
    }
}
