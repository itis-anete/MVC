using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketplaceMVC.Filters
{
    public class HtmlFilterResult : Attribute, IAsyncResultFilter, IResultFilter
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
            string action = context.ActionDescriptor.RouteValues["action"];
            string controller = context.ActionDescriptor.RouteValues["controller"];
            
            if (ContainsText(Environment.CurrentDirectory + $"\\{controller}\\{action}\\cshtml.{action}.cshtml")) throw new Exception($"Contains text(cshtml.{action}.cshtml)");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
        
        bool ContainsText(string dir)
        {
            bool containsText = false;
            bool sharpCode = false;
            foreach (var readLine in File.ReadAllText(dir))
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

                if (containsText) return true;
            }
            return false;
        }
    }
}
