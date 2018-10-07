using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing.Logging;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Resources;

namespace Route
{
    public class RouteBuilder : IRouteBuilder
    {
        public RouteBuilder(IApplicationBuilder applicationBuilder, IRouter defaultHandler)
        {

            ApplicationBuilder = applicationBuilder;
            DefaultHandler = defaultHandler;
            ServiceProvider = applicationBuilder.ApplicationServices;

            Routes = new List<IRouter>();
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }
        public IList<IRouter> Routes { get; }

        public IRouter Build()
        {
            var routeCollection = new RouteCollection();

            foreach (var route in Routes)
            {
                routeCollection.Add(route);
            }

            return routeCollection;
        }
    }
}
