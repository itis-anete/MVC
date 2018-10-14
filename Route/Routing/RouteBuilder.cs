using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace Route.Routing
{
    public class RouteBuilder : IRouteBuilder
    {
        private readonly IApplicationBuilder app;

        public RouteBuilder(IApplicationBuilder app)
        {
            this.app = app;
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public IList<IRouter> Routes { get; }

        public IRouter Build()
        {
            throw new NotImplementedException();
        }
    }
}
