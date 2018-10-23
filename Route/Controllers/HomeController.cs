using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Route.Models;
using Route.Filters;

namespace Route.Controllers
{
    [ActionFilter]
    public class HomeController : Controller, IInfoSystemController
    {
        public IActionResult Index() //[FromBody] int param)
        {
            //return !ModelState.IsValid ? Error() : View();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Page Title"] = "My App";
            ViewData["Object"] = new {Title = "Point", x = 1, y = 2};
            ViewBag.PageTitle = "New Title";

            return View(new ViewModel
            {
                Name = "123",
                Options = new List<KeyValue>
                {
                    new KeyValue
                    {
                        Key = 1,
                        Value = "123123"
                    }
                }
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult Test(
            [FromInfoSystemSpec] int x, // привязывается
            string y) // не привязывается)
        {
            throw new NotImplementedException();
        }

        public IActionResult Test2(int a)
        {
            return new JsonResult(new {Title = "title"});
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}