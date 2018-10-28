using Cannabis.ActionResults;
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
            [CannabisValidValue(typeof(int))]
            public CannabisValue TestCannabis { get; set; }
        }
        
        public IActionResult Test([FromCannabisSpec] PostModel parameter)
        {
            if (!ModelState.IsValid)
                parameter.Value = "You are invalid";
            return ActionResults.JsonResult.Create(parameter);
        }

        public IActionResult Test2([FromCannabisSpec] int parameter)
        {
            if (!ModelState.IsValid)
                parameter = 666;
            return RawResult.Create(parameter);
        }

        public IActionResult OutThroughWindow()
        {
            throw new ArgumentOutOfRangeException("patience");
        }

        public IActionResult GetFacked(int count)
        {
            return RawResult.Create($"You were facked {count} times");
        }

        public IActionResult TestView()
        {
            return new ActionResultWrapper(View());
        }
    }
}
