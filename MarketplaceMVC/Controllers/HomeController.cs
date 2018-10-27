using System;
using System.Diagnostics;
using MarketplaceMVC.ActionResults;
using MarketplaceMVC.Filters;
using MarketplaceMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers
{
    public class HomeController : Controller, IMarketplaceController
    {
        public static int CallCounter { get; set; }

        public HomeController()
        {
            CallCounter++;
        }

        [HtmlFilterResult]
        public IActionResult Index()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View());

        }

        [HtmlFilterResult]
        public IActionResult About()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View());
        }

        [ReverseExceptionFilter]
        public IActionResult Contact()
        {
            ViewData["CallCounter"] = CallCounter;

            try
            {
                var y = 0;
                var x = 5 / y;
            }
            catch
            {
                throw new DivideByZeroException();
            }

            return new MarketplaceActionResult(View());
        }

        public IActionResult Privacy()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public MarketplaceActionResult Marketplace()
        {
            ViewData["CallCounter"] = CallCounter;
            
            return new MarketplaceActionResult(View(new MarketplaceValue("Kek")));
        }
    }
}
