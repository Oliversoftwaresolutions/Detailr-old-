using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.RadioSelect
{
    public class BusinessRadioSelectViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
