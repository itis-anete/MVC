using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumLesson16
{
    public class CustomViewEngine : IRazorViewEngine, IViewEngine
    {
        public RazorPageResult FindPage(ActionContext context, string pageName)
        {
            throw new NotImplementedException();
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            throw new NotImplementedException();
        }

        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            throw new NotImplementedException();
        }

        public RazorPageResult GetPage(string executingFilePath, string pagePath)
        {
            throw new NotImplementedException();
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            throw new NotImplementedException();
        }
    }
}
