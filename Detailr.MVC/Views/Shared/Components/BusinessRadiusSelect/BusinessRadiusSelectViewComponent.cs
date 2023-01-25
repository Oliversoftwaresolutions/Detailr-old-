using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.RadiusSelect
{
    public class BusinessRadiusSelectViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
