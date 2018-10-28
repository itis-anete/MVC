using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ForumLesson16
{
    interface IHomeForumController
    {
        Task<IActionResult> Action();
    }
}