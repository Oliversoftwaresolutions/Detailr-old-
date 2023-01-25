using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.BusinessSearch
{
    public class BusinessSearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
