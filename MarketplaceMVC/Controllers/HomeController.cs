using System.Diagnostics;
using MarketplaceMVC.Actions;
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
            return View();
        }

        [HtmlFilterResult]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            ViewData["CallCounter"] = CallCounter;

            return View();
        }

        [ReverseExceptionFilter]
        public IActionResult Contact()
        {
            var y = 0;
            var x = 5 / y;
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public TitleActionResult HelloWorld()
        {
            return new TitleActionResult(View());
        }

    }
}
