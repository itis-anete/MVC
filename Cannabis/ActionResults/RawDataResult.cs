using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public class RawDataResult<T> : CannabisResult
    {
        public T Data { get; }

        public RawDataResult(T data)
        {
            Data = data;
        }

        protected override async Task ExecuteResultAsyncInternal(ActionContext context)
        {
            var responseString = Data.ToString();
            await context.HttpContext.Response.WriteAsync(responseString);
        }
    }
}
