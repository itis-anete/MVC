using Cannabis.Attributes;
using Cannabis.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cannabis.Controllers
{
    public class TestController : CannabisController
    {
        public class PostModel : CannabisValueModel
        {
            [CannabisValidValue(typeof(string))]
            public CannabisValue TestCannabis { get; set; }
        }

        [HttpPost]
        public void Post([FromCannabisSpec] PostModel parameter)
        {
            Console.WriteLine(parameter.TestCannabis);
        }
    }
}
