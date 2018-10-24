using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Route
{
    public class InfoSystemViewEngine : RazorViewEngine, IInfoSystemViewEngine, IViewEngine
    {
        public static readonly string ViewExtension = ".cshtml";
        private const string ViewStartFileName = "_ViewStart.cshtml";

        private const string AreaKey = "area";
        private const string ControllerKey = "controller";
        private const string PageKey = "page";

        private static readonly TimeSpan _cacheExpirationDuration = TimeSpan.FromMinutes(20);

        private readonly IRazorPageFactoryProvider _pageFactory;
        private readonly IRazorPageActivator _pageActivator;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger _logger;
        private readonly RazorViewEngineOptions _options;
        private readonly RazorProject _razorFileSystem;
        private readonly DiagnosticSource _diagnosticSource;

        public InfoSystemViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProject razorProject,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) : base(pageFactory,
            pageActivator,
            htmlEncoder,
            optionsAccessor,
            razorProject,
            loggerFactory,
            diagnosticSource)
        {
            _options = optionsAccessor.Value;

            if (_options.ViewLocationFormats.Count == 0)
            {
                throw new ArgumentException(nameof(optionsAccessor));
            }

            _options.ViewLocationFormats.Clear();
            _options.ViewLocationFormats.Add("/Views/InfoSystem/{1}/{0}/cshtml.{0}.cshtml");
            _options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            if (_options.AreaViewLocationFormats.Count == 0)
            {
                throw new ArgumentException(nameof(optionsAccessor));
            }

            _pageFactory = pageFactory;
            _pageActivator = pageActivator;
            _htmlEncoder = htmlEncoder;
            _logger = loggerFactory.CreateLogger<RazorViewEngine>();
            _razorFileSystem = razorProject;
            _diagnosticSource = diagnosticSource;
            ViewLookupCache = new MemoryCache(new MemoryCacheOptions());
        }

        /*public InfoSystemViewEngine(IRazorPageFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            RazorProjectFileSystem razorFileSystem,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource) : base(pageFactory,
            pageActivator,
            htmlEncoder,
            optionsAccessor,
            razorFileSystem,
            loggerFactory,
            diagnosticSource)
        {
        }*/

        protected IMemoryCache ViewLookupCache { get; }

        /// <inheritdoc />
        public new ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrEmpty(viewName))
                throw new ArgumentException(nameof(viewName));

            if (IsApplicationRelativePath(viewName) || IsRelativePath(viewName))
                return ViewEngineResult.NotFound(viewName, Enumerable.Empty<string>());

            var cacheResult = LocatePageFromViewLocations(context, viewName, isMainPage);
            return CreateViewEngineResult(cacheResult, viewName);
        }

        /// <inheritdoc />
        public new ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            if (string.IsNullOrEmpty(viewPath))
                throw new ArgumentException(nameof(viewPath));

            if (!(IsApplicationRelativePath(viewPath) || IsRelativePath(viewPath)))
                return ViewEngineResult.NotFound(viewPath, Enumerable.Empty<string>());

            var cacheResult = LocatePageFromPath(executingFilePath, viewPath, isMainPage);
            return CreateViewEngineResult(cacheResult, viewPath);
        }

        private ViewLocationCacheResult LocatePageFromPath(string executingFilePath, string pagePath, bool isMainPage)
        {
            var applicationRelativePath = GetAbsolutePath(executingFilePath, pagePath);
            var cacheKey = new ViewLocationCacheKey(applicationRelativePath, isMainPage);

            if (ViewLookupCache.TryGetValue(cacheKey, out ViewLocationCacheResult cacheResult)) return cacheResult;

            var expirationTokens = new HashSet<IChangeToken>();
            cacheResult = CreateCacheResult(expirationTokens, applicationRelativePath, isMainPage);

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
            foreach (var expirationToken in expirationTokens)
                cacheEntryOptions.AddExpirationToken(expirationToken);

            if (cacheResult == null)
                cacheResult = new ViewLocationCacheResult(new[] {applicationRelativePath});

            cacheResult = ViewLookupCache.Set(
                cacheKey,
                cacheResult,
                cacheEntryOptions);

            return cacheResult;
        }

        private ViewLocationCacheResult LocatePageFromViewLocations(
            ActionContext actionContext,
            string pageName,
            bool isMainPage)
        {
            var controllerName = GetNormalizedRouteValue(actionContext, ControllerKey);
            var areaName = GetNormalizedRouteValue(actionContext, AreaKey);
            string razorPageName = null;
            if (actionContext.ActionDescriptor.RouteValues.ContainsKey(PageKey))
                razorPageName = GetNormalizedRouteValue(actionContext, PageKey);

            var expanderContext = new ViewLocationExpanderContext(
                actionContext,
                pageName,
                controllerName,
                areaName,
                razorPageName,
                isMainPage);
            Dictionary<string, string> expanderValues = null;

            if (_options.ViewLocationExpanders.Count > 0)
            {
                expanderValues = new Dictionary<string, string>(StringComparer.Ordinal);
                expanderContext.Values = expanderValues;

                foreach (var t in _options.ViewLocationExpanders)
                    t.PopulateValues(expanderContext);
            }

            var cacheKey = new ViewLocationCacheKey(
                expanderContext.ViewName,
                expanderContext.ControllerName,
                expanderContext.AreaName,
                expanderContext.PageName,
                expanderContext.IsMainPage,
                expanderValues);

            if (!ViewLookupCache.TryGetValue(cacheKey, out ViewLocationCacheResult cacheResult))
            {
                _logger.ViewLookupCacheMiss(cacheKey.ViewName, cacheKey.ControllerName);
                cacheResult = OnCacheMiss(expanderContext, cacheKey);
            }
            else
            {
                _logger.ViewLookupCacheHit(cacheKey.ViewName, cacheKey.ControllerName);
            }

            return cacheResult;
        }

        /// <inheritdoc />
        public new string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            if (string.IsNullOrEmpty(pagePath))
                return pagePath;

            if (IsApplicationRelativePath(pagePath))
                return pagePath;

            if (!IsRelativePath(pagePath))
                return pagePath;

            if (!string.IsNullOrEmpty(executingFilePath))
                return ViewEnginePath.CombinePath(executingFilePath, pagePath);
            var absolutePath = "/" + pagePath;
            return ViewEnginePath.ResolvePath(absolutePath);

        }

        // internal for tests
        internal IEnumerable<string> GetViewLocationFormats(ViewLocationExpanderContext context)
        {
            if (!string.IsNullOrEmpty(context.AreaName) &&
                !string.IsNullOrEmpty(context.ControllerName))
            {
                return _options.AreaViewLocationFormats;
            }

            if (!string.IsNullOrEmpty(context.ControllerName))
            {
                return _options.ViewLocationFormats;
            }

            if (!string.IsNullOrEmpty(context.AreaName) &&
                !string.IsNullOrEmpty(context.PageName))
            {
                return _options.AreaPageViewLocationFormats;
            }

            if (!string.IsNullOrEmpty(context.PageName))
            {
                return _options.PageViewLocationFormats;
            }

            return _options.ViewLocationFormats;
        }

        private ViewLocationCacheResult OnCacheMiss(
            ViewLocationExpanderContext expanderContext,
            ViewLocationCacheKey cacheKey)
        {
            var viewLocations = GetViewLocationFormats(expanderContext);

            viewLocations = _options.ViewLocationExpanders.Aggregate(viewLocations,
                (current, t) => t.ExpandViewLocations(expanderContext, current));

            ViewLocationCacheResult cacheResult = null;
            var searchedLocations = new List<string>();
            var expirationTokens = new HashSet<IChangeToken>();
            foreach (var location in viewLocations)
            {
                var path = string.Format(
                    CultureInfo.InvariantCulture,
                    location,
                    expanderContext.ViewName,
                    expanderContext.ControllerName,
                    expanderContext.AreaName);

                path = ViewEnginePath.ResolvePath(path);

                cacheResult = CreateCacheResult(expirationTokens, path, expanderContext.IsMainPage);
                if (cacheResult != null)
                {
                    break;
                }

                searchedLocations.Add(path);
            }

            if (cacheResult == null)
            {
                cacheResult = new ViewLocationCacheResult(searchedLocations);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(_cacheExpirationDuration);
            foreach (var expirationToken in expirationTokens)
            {
                cacheEntryOptions.AddExpirationToken(expirationToken);
            }

            return ViewLookupCache.Set(cacheKey, cacheResult, cacheEntryOptions);
        }

        internal ViewLocationCacheResult CreateCacheResult(
            HashSet<IChangeToken> expirationTokens,
            string relativePath,
            bool isMainPage)
        {
            var factoryResult = _pageFactory.CreateFactory(relativePath);
            var viewDescriptor = factoryResult.ViewDescriptor;
            if (viewDescriptor?.ExpirationTokens != null)
            {
                foreach (var t in viewDescriptor.ExpirationTokens)
                    expirationTokens.Add(t);
            }

            if (!factoryResult.Success) return null;
            var viewStartPages = isMainPage
                ? GetViewStartPages(viewDescriptor.RelativePath, expirationTokens)
                : Array.Empty<ViewLocationCacheItem>();
            if (viewDescriptor.IsPrecompiled)
            {
                _logger.PrecompiledViewFound(relativePath);
            }

            return new ViewLocationCacheResult(
                new ViewLocationCacheItem(factoryResult.RazorPageFactory, relativePath),
                viewStartPages);

        }

        private IReadOnlyList<ViewLocationCacheItem> GetViewStartPages(
            string path,
            HashSet<IChangeToken> expirationTokens)
        {
            var viewStartPages = new List<ViewLocationCacheItem>();

            foreach (var viewStartProjectItem in _razorFileSystem.FindHierarchicalItems(path, ViewStartFileName))
            {
                var result = _pageFactory.CreateFactory(viewStartProjectItem.FilePath);
                var viewDescriptor = result.ViewDescriptor;
                if (viewDescriptor?.ExpirationTokens != null)
                    foreach (var t in viewDescriptor.ExpirationTokens)
                        expirationTokens.Add(t);

                if (result.Success)
                    viewStartPages.Insert(0,
                        new ViewLocationCacheItem(result.RazorPageFactory, viewStartProjectItem.FilePath));
            }

            return viewStartPages;
        }

        private ViewEngineResult CreateViewEngineResult(ViewLocationCacheResult result, string viewName)
        {
            if (!result.Success)
            {
                return ViewEngineResult.NotFound(viewName, result.SearchedLocations);
            }

            var page = result.ViewEntry.PageFactory();

            var viewStarts = new IRazorPage[result.ViewStartEntries.Count];
            for (var i = 0; i < viewStarts.Length; i++)
            {
                var viewStartItem = result.ViewStartEntries[i];
                viewStarts[i] = viewStartItem.PageFactory();
            }

            var view = new RazorView(this, _pageActivator, viewStarts, page, _htmlEncoder, _diagnosticSource);
            return ViewEngineResult.Found(viewName, view);
        }

        private static bool IsApplicationRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            return name[0] == '~' || name[0] == '/';
        }

        private static bool IsRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            return name.EndsWith(ViewExtension, StringComparison.OrdinalIgnoreCase);
        }
        
        
        #region Hide

/*

        public static string GetNormalizedRouteValue(ActionContext context, string key)
            => NormalizedRouteValue.GetNormalizedRouteValue(context, key);

        /// <inheritdoc />
        public new RazorPageResult FindPage(ActionContext context, string pageName)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrEmpty(pageName))
            {
                throw new ArgumentException( /*Resources.ArgumentCannotBeNullOrEmpty, #1#nameof(pageName));
            }

            if (IsApplicationRelativePath(pageName) || IsRelativePath(pageName))
            {
                // A path; not a name this method can handle.
                return new RazorPageResult(pageName, Enumerable.Empty<string>());
            }

            var cacheResult = LocatePageFromViewLocations(context, pageName, isMainPage: false);
            if (cacheResult.Success)
            {
                var razorPage = cacheResult.ViewEntry.PageFactory();
                return new RazorPageResult(pageName, razorPage);
            }
            else
            {
                return new RazorPageResult(pageName, cacheResult.SearchedLocations);
            }
        }

        /// <inheritdoc />
        public new RazorPageResult GetPage(string executingFilePath, string pagePath)
        {
            if (string.IsNullOrEmpty(pagePath))
            {
                throw new ArgumentException( /*Resources.ArgumentCannotBeNullOrEmpty,#1# nameof(pagePath));
            }

            if (!(IsApplicationRelativePath(pagePath) || IsRelativePath(pagePath)))
            {
                // Not a path this method can handle.
                return new RazorPageResult(pagePath, Enumerable.Empty<string>());
            }

            var cacheResult = LocatePageFromPath(executingFilePath, pagePath, isMainPage: false);
            if (cacheResult.Success)
            {
                var razorPage = cacheResult.ViewEntry.PageFactory();
                return new RazorPageResult(pagePath, razorPage);
            }
            else
            {
                return new RazorPageResult(pagePath, cacheResult.SearchedLocations);
            }
        }*/

        #endregion

    }
}