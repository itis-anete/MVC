using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace ForumLesson16
{
    public class ForumViewEngine : RazorViewEngine
    {
        public ForumViewEngine(
            IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProjectFileSystem razorFileSystem,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) :
            base(
                pageFactory,
                pageActivator,
                htmlEncoder,
                optionsAccessor,
                razorFileSystem,
                loggerFactory,
                diagnosticSource)
        {
            var options = optionsAccessor.Value;

            if (options.ViewLocationFormats.Count == 0)
                throw new ArgumentException(nameof(optionsAccessor));

            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("/Forum/{1}/{0}/cshtml.{0}.cshtml");
        }
    }
}