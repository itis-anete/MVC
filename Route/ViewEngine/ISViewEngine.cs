using System;
using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Route
{
    public class InfoSystemViewEngine : RazorViewEngine, IInfoSystemViewEngine
    {
        public InfoSystemViewEngine(IRazorPageFactoryProvider pageFactory,
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
            options.ViewLocationFormats.Add("/Views/InfoSystem/{1}/{0}/cshtml.{0}.cshtml");
            options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
        }

        /*public InfoSystemViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            throw new NotImplementedException();
        }

        public InfoSystemViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            throw new NotImplementedException();
        }*/
        
    }
}