using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cannabis.ActionResults
{
    public class ViewResult : CannabisResult
    {
        public Microsoft.AspNetCore.Mvc.ViewResult View { get; }

        public ViewResult(Microsoft.AspNetCore.Mvc.ViewResult viewResult)
        {
            View = viewResult ?? throw new ArgumentNullException(nameof(viewResult));
        }

        protected override async Task ExecuteResultAsyncInternal(ActionContext context)
        {
            await View.ExecuteResultAsync(context);
        }
    }
}
