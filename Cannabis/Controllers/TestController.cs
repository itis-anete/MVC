using Cannabis.ActionResults;
using Cannabis.Attributes;
using Cannabis.Models;
using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult<PostModel>(parameter);
        }

        public IActionResult Test2([FromCannabisSpec] int parameter)
        {
            if (!ModelState.IsValid)
                parameter = 666;
            return new RawDataResult<int>(parameter);
        }

        public IActionResult GetFacked(int count)
        {
            return new RawDataResult<string>($"You were facked {count} times");
        }
    }
}
