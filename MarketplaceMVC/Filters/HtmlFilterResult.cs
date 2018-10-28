using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketplaceMVC.Filters
{
    //public class HtmlFilterResult : Attribute, IResultFilter
    //{
    //    //КАКОЙ-ТО БРЕД ПЕРЕДЕЛАЙ

    //    string result;

    //    public void OnResultExecuted(ResultExecutedContext context)
    //    {
    //    }

    //    public void OnResultExecuting(ResultExecutingContext context)
    //    {

    //        string html = "";
    //        string layout = "_Layout.cshtml";
    //        string path = Path.Combine(Environment.CurrentDirectory, @"Views\", @"Shared\", layout);
    //        if (File.Exists(path))
    //            html = new StreamReader(path).ReadToEnd();
    //        else
    //        {
    //            path = Path.Combine(Environment.CurrentDirectory, @"Views\", context.ActionDescriptor.RouteValues.First(x => x.Key == "controller").Value + @"\", layout);
    //            if (File.Exists(path))
    //                html = new StreamReader(path).ReadToEnd();
    //            else
    //            {
    //                context.Result = new ContentResult { Content = "html not found" };
    //            }
    //        }
    //        string pattern = @"^<html>[\d\D]*</html>$";

    //        Regex regex = new Regex(pattern);
    //        if (!regex.Match(html).Success)
    //        {
    //            context.Result = new ContentResult { Content = "Something outside html tags" };
    //        }
    //    }
    //}
}
