using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route
{
    public interface ITimer
    {
        DateTime Value { get; }
    }


    public class NowTime : ITimer
    {
        private DateTime _value;
        public NowTime()
        {
            _value = DateTime.Now;
        }
        public DateTime Value { get { return _value; } }
    }

    public class TimerService
    {
        protected internal ITimer Timer { get; }
        public TimerService(ITimer timer)
        {
            Timer = timer;    
        }
    }

    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        public TimerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITimer timer, TimerService timerService)
        {
            Program.GetProp().Value = timer.Value;
            await _next.Invoke(httpContext);
        }
    }
}
