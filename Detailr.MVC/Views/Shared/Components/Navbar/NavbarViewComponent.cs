using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
