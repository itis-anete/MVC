using Microsoft.AspNetCore.Mvc;

namespace Cannabis.Controllers
{
    public class CannabisController : Controller, ICannabisController
    {
        public int CallCounter { get; set; }
    }
}
