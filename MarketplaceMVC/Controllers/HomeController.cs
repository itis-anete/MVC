using System;
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

        public MarketplaceValue Name { get; set; } 

        [HtmlFilterResult]
        public IActionResult Index()
        {
            ViewData["CallCounter"] = CallCounter;

            return View();

        }

        [HtmlFilterResult]
        public IActionResult About()
        {
            ViewData["CallCounter"] = CallCounter;

            return View();
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


            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["CallCounter"] = CallCounter;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public TitleActionResult HelloWorld()
        {
            ViewData["CallCounter"] = CallCounter;

            Name = new MarketplaceValue(new {Kek = 1});

            return new TitleActionResult(View(Name));
        }

    }
}
