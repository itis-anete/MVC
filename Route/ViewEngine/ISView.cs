using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Route
{
    public class InfoSystemView : RazorView
    {
        public InfoSystemView(IRazorViewEngine viewEngine,
            IRazorPageActivator pageActivator,
            IReadOnlyList<IRazorPage> viewStartPages,
            IRazorPage razorPage,
            HtmlEncoder htmlEncoder,
            DiagnosticSource diagnosticSource) : base(viewEngine,
            pageActivator,
            viewStartPages,
            razorPage,
            htmlEncoder,
            diagnosticSource)
        {
        }
    }
}