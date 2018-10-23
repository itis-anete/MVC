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
        }

        // Компилятор не понял разных конструкторов ?
        // "Application startup exception:
        // System.InvalidOperationException: Unable to activate type 'Route.InfoSystemViewEngine'.
        // The following constructors are ambiguous"
        /*public InfoSystemViewEngine(IRazorPageFactoryProvider pageFactory,
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
        }*/
        
        public new ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            // not used
            
            var controllerName = context.RouteData.Values.Values.First();
            var viewPath = $"InfoSystem/{controllerName}/{viewName}/cshtml.{viewName}.cshtml"; //$"Views/{controllerName}/{viewName}.cshtml";
            if (string.IsNullOrEmpty(viewName))
            {
                viewPath = $"Views/{controllerName}/{context.RouteData.Values["action"]}.cshtml";
            }

            /*return File.Exists(viewPath)
                ? InfoSystemViewEngineResult.Found(viewPath, new InfoSystemView(viewPath))
                : InfoSystemViewEngineResult.NotFound(viewName, new[] {viewPath});*/
            return File.Exists(viewPath)
                ? ViewEngineResult.Found(viewPath, new InfoSystemView(viewPath))
                : ViewEngineResult.NotFound(viewName, new[] {viewPath});
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            throw new System.NotImplementedException();
        }

        public RazorPageResult FindPage(ActionContext context, string pageName)
        {
            throw new System.NotImplementedException();
        }

        public RazorPageResult GetPage(string executingFilePath, string pagePath)
        {
            throw new System.NotImplementedException();
        }

        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            throw new System.NotImplementedException();
        }
    }
}