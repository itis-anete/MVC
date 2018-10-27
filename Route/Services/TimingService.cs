using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Route.Services
{
    public class TimingService
    {
        private readonly RequestDelegate _next;

        public TimingService(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Router.Start.Value = DateTime.Now;
            await _next.Invoke(httpContext);
        }
    }
}