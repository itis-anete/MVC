using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public class JsonResult<T> : CannabisResult
    {
        public T Data { get; }

        public JsonResult(T data)
        {
            Data = data;
        }

        protected override async Task ExecuteResultAsyncInternal(ActionContext context)
        {
            var responseString = JsonConvert.SerializeObject(Data);
            await context.HttpContext.Response.WriteAsync(responseString);
        }
    }
}
