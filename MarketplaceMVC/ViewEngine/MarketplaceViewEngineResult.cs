using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MarketplaceMVC.ViewEngine
{
    public class MarketplaceViewEngineResult
    {
        private MarketplaceViewEngineResult()
        {
        }

        public IEnumerable<string> SearchedLocations { get; private set; }
        public IView View { get; private set; }
        public string ViewName { get; private set; }
        public bool Success => View != null;

        public static MarketplaceViewEngineResult NotFound(
            string viewName,
            IEnumerable<string> searchedLocations)
        {
            if (!File.Exists(searchedLocations + viewName))
                throw new ArgumentNullException(nameof(viewName));

            if (searchedLocations == null)
                throw new ArgumentNullException(nameof(searchedLocations));

            return new MarketplaceViewEngineResult
            {
                SearchedLocations = searchedLocations,
                ViewName = viewName,
            };
        }

        public static MarketplaceViewEngineResult Found(string viewName, IView view)
        {
            if (viewName == null)
                throw new ArgumentNullException(nameof(viewName));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return new MarketplaceViewEngineResult
            {
                View = view,
                ViewName = viewName,
            };
        }
    }
}
