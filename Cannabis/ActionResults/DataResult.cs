using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cannabis.ActionResults
{
    public abstract class DataResult<T> : CannabisResult
    {
        public T Data { get; }

        public DataResult(T data)
        {
            Data = data;
        }

        protected override async Task ExecuteResultAsyncInternal(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ContentType;
            await response.WriteAsync(DataAsString);
        }

        protected abstract string DataAsString { get; }
        protected abstract string ContentType { get; }
    }
}
