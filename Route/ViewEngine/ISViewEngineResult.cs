using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Route
{
    public class InfoSystemViewEngineResult
    {
        public IEnumerable<string> SearchedLocations { get; private set; }
        public IView View { get; private set; }
        public string ViewName { get; private set; }
        public bool Success => View != null;

        public static InfoSystemViewEngineResult NotFound(
            string viewName,
            IEnumerable<string> searchedLocations)
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            if (searchedLocations == null)
            {
                throw new ArgumentNullException(nameof(searchedLocations));
            }

            return new InfoSystemViewEngineResult
            {
                SearchedLocations = searchedLocations,
                ViewName = viewName,
            };
        }

        public static InfoSystemViewEngineResult Found(string viewName, IView view)
        {
            if (viewName == null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            return new InfoSystemViewEngineResult
            {
                View = view,
                ViewName = viewName,
            };
        }
    }
}