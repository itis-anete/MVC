using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketplaceMVC.NewViewEngine
{
    public class CustomViewEngineResult
    {
        private CustomViewEngineResult()
        {
        }

        public IEnumerable<string> SearchedLocations { get; private set; }
        public IView View { get; private set; }
        public string ViewName { get; private set; }
        public bool Success => View != null;

        public static CustomViewEngineResult NotFound(
            string viewName,
            IEnumerable<string> searchedLocations)
        {
            if (!File.Exists(searchedLocations + viewName))
                throw new ArgumentNullException(nameof(viewName));

            if (searchedLocations == null)
                throw new ArgumentNullException(nameof(searchedLocations));

            return new CustomViewEngineResult
            {
                SearchedLocations = searchedLocations,
                ViewName = viewName,
            };
        }

        public static CustomViewEngineResult Found(string viewName, IView view)
        {
            if (viewName == null)
                throw new ArgumentNullException(nameof(viewName));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return new CustomViewEngineResult
            {
                View = view,
                ViewName = viewName,
            };
        }
    }
}
