using MarketplaceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceMVC.ActionResults
{
    public class MarketplaceResActionResult : IActionResult
    {
        ViewResult View { get; set; }

        public MarketplaceResActionResult(ViewResult view, Dictionary<string, object> viewData)
        {
            View = view;
            foreach (var key in viewData.Keys)
                View.ViewData[key] = viewData[key];
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await View.ExecuteResultAsync(context);
        }
    }
}
