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
            return new ActionResults.ViewResult(View());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return new ActionResults.ViewResult(View());
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return new ActionResults.ViewResult(View());
        }
        
        public IActionResult Privacy()
        {
            return new ActionResults.ViewResult(View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return new ActionResults.ViewResult(
                View(new ErrorViewModel { RequestId = new CannabisValue(Activity.Current?.Id ?? HttpContext.TraceIdentifier) })
            );
        }
    }
}
