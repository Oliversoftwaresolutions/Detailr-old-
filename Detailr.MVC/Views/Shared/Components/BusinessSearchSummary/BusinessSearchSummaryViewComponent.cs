using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Views.Shared.Components.BusinessSearchSummary
{
    public class BusinessSearchSummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
