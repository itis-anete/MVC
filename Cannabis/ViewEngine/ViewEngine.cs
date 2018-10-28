using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Cannabis.ViewEngine
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProject razorProject,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource
        ) : base(
                pageFactory,
                pageActivator,
                htmlEncoder,
                optionsAccessor,
                razorProject,
                loggerFactory,
                diagnosticSource)
        {
            var viewLocations = optionsAccessor.Value.ViewLocationFormats;
            viewLocations.Clear();

            var viewFolder = "/" + ProjectInfo.ProjectName;
            viewLocations.Add(viewFolder + @"/{1}/{0}/cshtml.{0}.cshtml");
            viewLocations.Add(viewFolder + @"/{0}.cshtml");
        }
    }
}