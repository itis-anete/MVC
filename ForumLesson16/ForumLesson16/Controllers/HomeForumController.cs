using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class HomeForumController : ForumControllerBase, IHomeForumController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //context -> IDictionary<string, object> ActionArguments
            base.OnActionExecuting(context);
        }

        [HttpGet]
        public async Task<IActionResult> Action()
        {
            Counter++;
            return await Task.FromResult(new ForumActionResult());
        }

        [HttpPost]
        public async Task<IActionResult> Action([FromForumSpec]ForumModel forumModel)
        {
            Counter++;
            return await Task.FromResult(new ForumActionResult());
        }
    }
}