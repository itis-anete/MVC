using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Models;
using Route.Filters;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Route.Controllers
{
    public class HomeController : Controller, IInfoSystemController
    {
        private static int CallCount;
        private InfoSystemValue Value { get; set; }

        public HomeController()
        {
            CallCount++;
        }

        //[HttpGet]
        public InfoSystemActionResult Index()
        {
            return new InfoSystemActionResult(View());
        }

        //[HttpGet]
        public InfoSystemActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Page Title"] = "My App";
            ViewData["Object"] = new {Title = "Point", x = 1, y = 2};
            ViewBag.PageTitle = "New Title";

            return new InfoSystemActionResult(View(new ViewModel
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
            }));
        }

        public InfoSystemActionResult Privacy()
        {
            return new InfoSystemActionResult(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public InfoSystemActionResult Error()
        {
            return new InfoSystemActionResult(View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            }));
        }

        public InfoSystemActionResult Contact([FromInfoSystemSpec] InfoSystemValueModel model1)
        {
            return new InfoSystemActionResult(View());
        }

        //[ActionFilter]
        //[ExceptionFilter]
        [ResultFilter]
        public InfoSystemActionResult MyGet()
        {
            try
            {
                var xx = Request.Path.Value.Split('/')[3];
                if (xx == string.Empty) throw new ArgumentException();
                Value = new InfoSystemValue(xx);
            }
            catch
            {
                Value = new InfoSystemValue(new object());
            }

            return new InfoSystemActionResult(View(Value));
        }

        public void GetCount([FromInfoSystemSpec] InfoSystemValueModel str)
        {
            Response.WriteAsync(CallCount.ToString());
        }
    }
}