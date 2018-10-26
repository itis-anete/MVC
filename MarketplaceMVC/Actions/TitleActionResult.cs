using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceMVC.Actions
{
    public class TitleActionResult : IActionResult
    {
        public string Project { get; private set; }

        public ViewResult View { get; }

        public TitleActionResult(ViewResult view)
        {
            View = view;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            Project = Assembly.GetCallingAssembly().GetName().Name;
            if (View.ViewData.ContainsKey("Title"))
                View.ViewData["Title"] = Project;
            else
                View.ViewData.Add("Title", Project);
            await View.ExecuteResultAsync(context);
            
        }
    }
}
