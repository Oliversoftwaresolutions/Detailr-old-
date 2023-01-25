using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.BusinessSearchUserDetails
{
    public class BusinessSearchUserDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
