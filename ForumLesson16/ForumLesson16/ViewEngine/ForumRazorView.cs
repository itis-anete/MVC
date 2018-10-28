using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace ForumLesson16
{
    public class ForumRazorView : RazorView
    {
        public ForumRazorView(
            IRazorViewEngine viewEngine, 
            IRazorPageActivator pageActivator, 
            IReadOnlyList<IRazorPage> viewStartPages, 
            IRazorPage razorPage, 
            HtmlEncoder htmlEncoder, 
            DiagnosticSource diagnosticSource) : 
            base(
                viewEngine, 
                pageActivator, 
                viewStartPages, 
                razorPage, 
                htmlEncoder, 
                diagnosticSource)
        {       
        }
    }
}