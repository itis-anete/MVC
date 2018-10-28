using Microsoft.AspNetCore.Mvc;

namespace ForumLesson16
{
    public abstract class ForumControllerBase : Controller, IForumController
    {
        public int RequestCounter { get; protected set; }
    }
}
