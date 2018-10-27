using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public class CannabisResult : IActionResult
    {
        public CannabisResult(object data = null)
        {
            _data = data;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext.Response;
            response.Headers["ServerApplication"] = ProjectInfo.ProjectName;

            var responseString = JsonConvert.SerializeObject(_data);
            await response.WriteAsync(responseString);
        }

        private readonly object _data;
    }
}
