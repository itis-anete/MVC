using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Internal;
using System;
using System.Collections.Generic;

namespace Route
{
    public class RouteBuilder : IRouteBuilder
    {
        public RouteBuilder(IApplicationBuilder applicationBuilder, IRouter defaultHandler)
        {
            if (applicationBuilder == null)
                throw new ArgumentNullException(nameof(applicationBuilder));
            if (applicationBuilder.ApplicationServices.GetService(typeof(RoutingMarkerService)) == null)
                throw new InvalidOperationException();

            ApplicationBuilder = applicationBuilder;
            DefaultHandler = defaultHandler;
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public IList<IRouter> Routes { get; }

        public IRouter Build()
        {
            var routeCollection = new RouteCollection();
            foreach (var route in Routes)
                routeCollection.Add(route);
            return routeCollection;
        }
    }
}