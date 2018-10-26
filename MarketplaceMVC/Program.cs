using System;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MarketplaceMVC
{
    public class Program
    {
        public static ThreadLocal<DateTime> RequestTime { get; set; } = new ThreadLocal<DateTime>(()=>DateTime.Now);

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
