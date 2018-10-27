using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Route
{
    public class InfoSystemActionResult : IActionResult
    {
        private ViewResult _view;

        public InfoSystemActionResult()
        {
            
        }
        public InfoSystemActionResult(ViewResult view)
        {
            _view = view;
        }
        
        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add("ServerApplication", "InfoSystem");
            await _view.ExecuteResultAsync(context);
        }
    }
}