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
        [HttpGet]
        public async Task<IActionResult> TestForm()
        {
            RequestCounter++;
            return await Task.FromResult(View("TestForm"));
        }

        [HttpPost]
        public async Task<IActionResult> TestForm([FromForumSpec]ForumModel forumModel)
        {
            RequestCounter++;
            return await Task.FromResult(new ForumActionResult(forumModel.ToString()));
        }

        [HttpGet]
        public async Task<IActionResult> Action()
        {
            RequestCounter++;
            return await Task.FromResult(new ForumActionResult(RequestCounter.ToString()));
        }
    }
}