using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;

namespace ForumLesson16
{
    public class ForumViewEngineResult
    {
        private ForumViewEngineResult()
        { }

        public IEnumerable<string> SearchedLocations { get; private set; }
        public IView View { get; private set; }
        public string ViewName { get; private set; }
        public bool Success => View != null;

        public static ForumViewEngineResult NotFound(string viewName, IEnumerable<string> searchedLocations)
        {
            if (viewName == null)
                throw new ArgumentNullException(nameof(viewName));

            if (searchedLocations == null)
                throw new ArgumentNullException(nameof(searchedLocations));

            return new ForumViewEngineResult
            {
                SearchedLocations = searchedLocations,
                ViewName = viewName,
            };
        }

        public static ForumViewEngineResult Found(string viewName, IView view)
        {
            if (viewName == null)
                throw new ArgumentNullException(nameof(viewName));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return new ForumViewEngineResult
            {
                View = view,
                ViewName = viewName,
            };
        }
    }
}