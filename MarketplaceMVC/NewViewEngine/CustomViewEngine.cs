using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MarketplaceMVC.NewViewEngine
{
    public class CustomViewEngine : RazorViewEngine, ICustomViewEngine
    {
        public CustomViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProject razorProject,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) : base(pageFactory,
            pageActivator,
            htmlEncoder,
            optionsAccessor,
            razorProject,
            loggerFactory,
            diagnosticSource)
        {
            var options = optionsAccessor.Value;

            if (options.ViewLocationFormats.Count == 0)
                throw new ArgumentException(nameof(optionsAccessor));

            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add(Environment.CurrentDirectory + "/{1}/{0}/cshtml.{0}.cshtml");
            //options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            //options.ViewLocationFormats.Add("/Views/");
        }
    }
}
