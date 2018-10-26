using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MarketplaceMVC.Middlewares
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;

        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Program.RequestTime.Value = DateTime.Now;
            await _next.Invoke(httpContext);
        }
    }
}
