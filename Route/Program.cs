using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Route
{
    public class Program
    {
        private static ThreadLocal<DateTime> prop;

        public static ThreadLocal<DateTime> GetProp()
        {
            return prop;
        }

        public static void SetProp(ThreadLocal<DateTime> value)
        {
            prop = value;
        }

        public static void Main(string[] args)
        {
            SetProp(new ThreadLocal<DateTime>(() => { return DateTime.Now; }));
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
