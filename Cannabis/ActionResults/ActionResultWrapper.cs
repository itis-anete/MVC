using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cannabis.ActionResults
{
    public class ActionResultWrapper : CannabisResult
    {
        public IActionResult Inner { get; }

        public ActionResultWrapper(IActionResult actionResult)
        {
            Inner = actionResult ?? throw new ArgumentNullException(nameof(actionResult));
        }

        protected override async Task ExecuteResultAsyncInternal(ActionContext context)
        {
            await Inner.ExecuteResultAsync(context);
        }
    }
}
