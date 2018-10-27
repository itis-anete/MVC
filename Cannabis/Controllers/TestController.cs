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
            [CannabisValidValue(typeof(string))]
            public CannabisValue TestCannabis { get; set; }
        }
        
        public IActionResult Test([FromCannabisSpec] PostModel parameter)
        {
            return new DataResult<PostModel>(parameter);
        }

        [HttpPost]
        public IActionResult Post([FromCannabisSpec] PostModel parameter)
        {
            return new DataResult<PostModel>(parameter);
        }
    }
}
