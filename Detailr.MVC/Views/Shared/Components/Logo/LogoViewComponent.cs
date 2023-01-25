using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.Logo
{
    public class LogoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
