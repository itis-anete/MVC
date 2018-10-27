﻿using System.Diagnostics;
using MarketplaceMVC.ActionResults;
using MarketplaceMVC.Filters;
using MarketplaceMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Controllers
{
    public class MarketplaceController : Controller, IMarketplaceController
    {
        public int CallCounter { get; set; }
        
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
            
            var y = 0;
            var x = 5 / y;

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

        [HttpGet]
        public MarketplaceActionResult Marketplace()
        {
            ViewData["CallCounter"] = CallCounter;

            var model = new MarketplaceModel {Age = new MarketplaceValue(20)};

            return new MarketplaceActionResult(View(model));
        }

        [HttpPost]
        public MarketplaceActionResult Marketplace(int number)
        {
            ViewData["CallCounter"] = CallCounter;

            var model = new MarketplaceModel { Age = new MarketplaceValue(10) };

            return new MarketplaceActionResult(View(model));
        }

        [HttpPut]
        public MarketplaceActionResult Marketplace(int number, int age)
        {
            ViewData["CallCounter"] = CallCounter;

            var model = new MarketplaceModel { Age = new MarketplaceValue(5) };

            return new MarketplaceActionResult(View(model));
        }
    }
}
