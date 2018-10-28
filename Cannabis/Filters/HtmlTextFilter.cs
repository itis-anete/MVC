using Cannabis.ActionResults;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cannabis.Filters
{
    public class HtmlTextFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Does not work :(
            //var result = new StreamReader(context.HttpContext.Response.GetResponseStream()).ReadToEnd();

            var action = context.ActionDescriptor.RouteValues["action"];
            var controller = context.ActionDescriptor.RouteValues["controller"];

            string result;
            try
            {
                result = File.ReadAllText( // FIX!!!
                    ProjectInfo.ProjectName + $"/{controller}/{action}/cshtml.{action}.cshtml");
            }
            catch
            {
                return;
            }

            if (ContainsText(result))
                context.Result = RawResult.Create("Result contained invalid text");
        }

        private bool ContainsText(string document) // FIX!!!
        {
            var containsText = false;
            var cSharpCode = false;
            var symbolStack = new Stack<char>();
            foreach (var symbol in document)
            {
                switch (symbol)
                {
                    case '{':
                    case '<':
                        cSharpCode = false;
                        symbolStack.Push(symbol);
                        continue;
                    case '}':
                    case '>':
                        symbolStack.Pop();
                        continue;
                    case '@':
                        cSharpCode = true;
                        continue;
                    case '\n' when cSharpCode && symbolStack.Count == 0:
                        cSharpCode = false;
                        continue;
                }

                if (cSharpCode || symbolStack.Count > 0)
                    continue;

                switch (symbol)
                {
                    case '\n':
                    case '\r':
                        continue;
                    default:
                        if (symbolStack.Count == 0)
                            containsText = true;
                        break;
                }

                if (containsText)
                    return true;
            }
            return false;
        }
    }
}