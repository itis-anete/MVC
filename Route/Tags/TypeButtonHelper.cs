using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Route.Tags
{
    public static class TypeButtonHelper
    {
        public static HtmlString TypeButton(this IHtmlHelper html, object dynamic)
        {
            string result = "<button>";
            result += dynamic.GetType();
            result += "</button>";
            return new HtmlString(result);
        }
    }
}
