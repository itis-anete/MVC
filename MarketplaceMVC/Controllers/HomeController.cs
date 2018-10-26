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
        public MarketplaceValue Age { get; set; }
        public MarketplaceValue Address { get; set; }
        public MarketplaceValue IsStudent { get; set; }
        public MarketplaceValue Leva { get; set; }

        [HtmlFilterResult]
        public IActionResult Index()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View(Name));

        }

        [HtmlFilterResult]
        public IActionResult About()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View(Name));
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

            return new MarketplaceActionResult(View(Name));
        }

        public IActionResult Privacy()
        {
            ViewData["CallCounter"] = CallCounter;

            return new MarketplaceActionResult(View(Name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public MarketplaceActionResult Marketplace()
        {
            ViewData["CallCounter"] = CallCounter;

            Name = new MarketplaceValue("Leva");
            Age = new MarketplaceValue(20);
            IsStudent = new MarketplaceValue(true);
            Address = new MarketplaceValue(new {City = "Kazan", Street = "Zilantovskaya", House = 18, Flat = 25});
            Leva = new MarketplaceValue(new {Name, Age, Address, IsStudent});

            return new MarketplaceActionResult(View(Leva));
        }

        public IActionResult TrueDirect()
        {
            return View();
        }
    }
}
