using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Route.Actions
{
    public class TitleActionResult : IActionResult
    {
        string project;
        ViewResult view;
        public TitleActionResult(ViewResult view)
        {
            this.view = view;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            project = Assembly.GetCallingAssembly().GetName().Name;
            if (view.ViewData.ContainsKey("Title"))
                view.ViewData["Title"] = project;
            else
                view.ViewData.Add("Title", project);
            await view.ExecuteResultAsync(context);
            
        }
    }
}
