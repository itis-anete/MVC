using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing.Internal;

namespace Route
{
    public class RouteBuilder : IRouteBuilder
    {
        public IApplicationBuilder ApplicationBuilder { get; }
        public IRouter DefaultHandler { get; set; }
        public IServiceProvider ServiceProvider { get; }
        public IList<IRouter> Routes { get; } = new List<IRouter>();

        public RouteBuilder(IApplicationBuilder builder, IRouter handler)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.ApplicationServices.GetService(typeof(RoutingMarkerService)) == null)
            {
                throw new InvalidOperationException();
            }

            ApplicationBuilder = builder;
            DefaultHandler = handler;
            ServiceProvider = builder.ApplicationServices;
        }

        public IRouter Build()
        {
            var rc = new RouteCollection();

            foreach (var route in Routes)
                rc.Add(route);

            return rc;
        }
    }
}