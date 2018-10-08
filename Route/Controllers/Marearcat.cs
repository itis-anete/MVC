using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Controllers
{
    public class Marearcat : Controller
    {
        public async Task PassivePage(HttpContext context)
        {
            await context.Response.WriteAsync("Welcome!");
        }

        public async Task Intro(HttpContext context)
        {
            await context.Response.WriteAsync("Hello, human!");
        }

        public async Task Ending(HttpContext context)
        {
            await context.Response.WriteAsync("Goodbye, human!");
        }
    }
}
