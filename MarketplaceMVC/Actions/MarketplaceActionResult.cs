using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Actions
{
    public class MarketplaceActionResult : IActionResult
    {
        public ViewResult View { get; }

        public MarketplaceActionResult(ViewResult view)
        {
            View = view;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("ServerApplication", "Marketplace");
            
            await View.ExecuteResultAsync(context);
        }
    }
}
