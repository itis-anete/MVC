using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Route.Filters
{
    public class HtmlFilterResult : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //todo
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            //todo

        }
    }
}
