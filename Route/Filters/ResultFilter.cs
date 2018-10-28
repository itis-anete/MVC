using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Route.Filters
{
    public class ResultFilter : Attribute, IAsyncResultFilter, IResultFilter
    {
        private readonly Stack<char> _symbolStack = new Stack<char>();
        
        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            OnResultExecuting(context);
            OnResultExecuted(next.Invoke().Result);
            return Task.CompletedTask;
        }

        public async void OnResultExecuting(ResultExecutingContext context)
        {
            var page = context.ActionDescriptor.RouteValues["action"];
            var containsText = false;
            var sharpCode = false;
            
            foreach (var readLine in File.ReadAllText($"../Route/InfoSystem/Home/{page}/cshtml.{page}.cshtml"))
            {
                switch (readLine)
                {
                    case '{':
                    case '<':
                        sharpCode = false;
                        _symbolStack.Push(readLine);
                        continue;
                    case '}':
                    case '>':
                        _symbolStack.Pop();
                        continue;
                    case '@':
                        sharpCode = true;
                        continue;
                    case '\n' when (sharpCode && _symbolStack.Count == 0):
                        sharpCode = false;
                        continue;
                }

                if (_symbolStack.Count != 0 || sharpCode) continue;

                switch (readLine)
                {
                    case '\n':
                    case '\r': 
                        continue;
                    default:
                        if (_symbolStack.Count == 0) containsText = true;
                        break;
                }

                if (containsText) break;
            }

            if (containsText) throw new Exception("Contains text");//  await context.HttpContext.Response.WriteAsync("Contains text");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}