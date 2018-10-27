using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public abstract class CannabisResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            
            context.HttpContext.Response.Headers["ServerApplication"] = ProjectInfo.ProjectName;

            await ExecuteResultAsyncInternal(context);
        }

        protected abstract Task ExecuteResultAsyncInternal(ActionContext context);
    }
}
