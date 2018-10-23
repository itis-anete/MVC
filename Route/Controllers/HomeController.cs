using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Route.Models;
using Newtonsoft.Json;

namespace Route.Controllers
{
    // [ActionFilterAttribute]
    public class HomeController : Controller, IInfoSystemController
    {
        public IActionResult Index() //[FromBody] int param)
        {
            //return !ModelState.IsValid ? Error() : View();
            return View();
        }

        [HttpGet]
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

        /*public InfoSystemActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return new InfoSystemActionResult();
        }*/

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

        [HttpGet]
        public IActionResult Test(
            [FromInfoSystemSpec] int x,    // привязывается
                                 string y) // не привязывается)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Test2([FromInfoSystemSpec] InfoSystemValue a)
        {
            return new JsonResult(new {Title = "title"});
        }

        public InfoSystemActionResult Contact()
        {
            return new InfoSystemActionResult();
        }
    }
}