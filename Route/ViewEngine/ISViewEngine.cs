using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Route
{
    public class InfoSystemViewEngine: RazorViewEngine, IRazorViewEngine
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
        }

        public InfoSystemViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProjectFileSystem razorFileSystem,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) : base(pageFactory,
            pageActivator,
            htmlEncoder,
            optionsAccessor,
            razorFileSystem,
            loggerFactory,
            diagnosticSource)
        {
        }
        
        public new InfoSystemViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            var controllerName = context.RouteData.Values.Values.First();
            var viewPath = $"InfoSystem/{controllerName}/{viewName}/cshtml.{viewName}.cshtml"; //$"Views/{controllerName}/{viewName}.cshtml";
            if (string.IsNullOrEmpty(viewName))
            {
                viewPath = $"Views/{controllerName}/{context.RouteData.Values["action"]}.cshtml";
            }

            return File.Exists(viewPath)
                ? InfoSystemViewEngineResult.Found(viewPath, new InfoSystemView(viewPath))
                : InfoSystemViewEngineResult.NotFound(viewName, new[] {viewPath});
        }
    }
}