using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public class DataResult<T> : CannabisResult
    {
        public T Data { get; }

        public DataResult(T data)
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
