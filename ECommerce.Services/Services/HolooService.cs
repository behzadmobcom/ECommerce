

using Services.IServices;

using System.Threading.Tasks;

namespace Services.Services
{
    public class HolooService : IHolooService
    {
        private readonly IHttpService _http;
        

        public HolooService(IHttpService http)
        {
            _http = http;
        }

        public async Task<string> ConvertHoloo(bool isAllMGroupConvert, string mGroupCode)
        {
            //var resultDialog = await _sweet.FireAsync(new SweetAlertOptions
            //{
            //    Title = "تبدیل از هلو",
            //    Text = "اگر قبلا کالایی را از این گروه یا گروه ها تبدیل کردید، تکراری ذخیره خواهد شد. آیا مایل به انجام هستید؟",
            //    Icon = SweetAlertIcon.Warning,
            //    ShowCancelButton = true,
            //    ConfirmButtonText = "بله، تبدیل کن",
            //    CancelButtonText = "نه، خودم دستی وارد میکنم"
            //});
            //if (resultDialog.Dismiss == DismissReason.Cancel) return false;
            var mCode = isAllMGroupConvert == false ? mGroupCode : "";
            var response = await _http.PostAsync("api/Products/ConvertHolooToSunflower", mCode);
            if (response.Code== 0)
            {
                return "با موفقیت تبدیل شد";
            }
            return response.GetBody();
        }

    }
}
