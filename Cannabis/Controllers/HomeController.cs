using Cannabis.ActionResults;
using Cannabis.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cannabis.Controllers
{
    public class HomeController : CannabisController
    {
        public IActionResult Index()
        {
            ViewData["CallCounter"] = CallCounter;
            return new ActionResultWrapper(View());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return new ActionResultWrapper(View());
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return new ActionResultWrapper(View());
        }
        
        public IActionResult Privacy()
        {
            return new ActionResultWrapper(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return new ActionResultWrapper(
                View(new ErrorViewModel { RequestId = new CannabisValue(Activity.Current?.Id ?? HttpContext.TraceIdentifier) })
            );
        }
    }
}
