using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public abstract class ForumControllerBase : Controller, IForumController
    {
        public int Counter { get; protected set; }
    }
}
