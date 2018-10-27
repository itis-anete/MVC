using System;
using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MarketplaceMVC.MarketplaceViewEngine
{
    public class MarketplaceViewEngine : RazorViewEngine, IMarketplaceViewEngine
    {
        public MarketplaceViewEngine(IRazorPageFactoryProvider pageFactory,
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
            options.ViewLocationFormats.Add(@"\{1}\{0}\cshtml.{0}.cshtml");
            options.ViewLocationFormats.Add(@"\Views\Shared\{0}.cshtml");
        }
    }
}
