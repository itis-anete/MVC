using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages.Internal;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Route
{
    public class ActionInvokerProvider : IActionInvokerProvider
    {

        public int Order { get; }

        private InnerCache CurrentCache
        {
            get
            {
                var current = _currentCache;
                var actionDescriptors = _collectionProvider.ActionDescriptors;

                if (current != null && current.Version == actionDescriptors.Version)
                    return current;
                current = new InnerCache(actionDescriptors.Version);
                _currentCache = current;

                return current;
            }
        }

        private readonly IPageLoader _loader;
        private readonly IPageFactoryProvider _pageFactoryProvider;
        private readonly IPageModelFactoryProvider _modelFactoryProvider;
        private readonly IModelBinderFactory _modelBinderFactory;
        private readonly IActionDescriptorCollectionProvider _collectionProvider;
        private readonly IFilterProvider[] _filterProviders;
        private readonly IReadOnlyList<IValueProviderFactory> _valueProviderFactories;
        private readonly ParameterBinder _parameterBinder;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ITempDataDictionaryFactory _tempDataFactory;
        private readonly HtmlHelperOptions _htmlHelperOptions;
        private readonly IPageHandlerMethodSelector _selector;
        private readonly DiagnosticSource _diagnosticSource;
        private readonly ILogger<PageActionInvoker> _logger;
        private readonly IActionResultTypeMapper _mapper;
        private volatile InnerCache _currentCache;

        public void OnProvidersExecuting(ActionInvokerProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!(context.ActionContext.ActionDescriptor is PageActionDescriptor actionDescriptor))
                return;

            var cache = CurrentCache;
            IFilterMetadata[] filters;

            if (!cache.Entries.TryGetValue(actionDescriptor, out var cacheEntry))
            {
                context.ActionContext.ActionDescriptor = _loader.Load(actionDescriptor);

                var filterFactoryResult = FilterFactory.GetAllFilters(_filterProviders, context.ActionContext);
                filters = filterFactoryResult.Filters;
                //cacheEntry = .............
                cacheEntry = cache.Entries.GetOrAdd(actionDescriptor, cacheEntry);
            }
            else
            {
                filters = FilterFactory.CreateUncachedFilters(
                    _filterProviders,
                    context.ActionContext,
                    cacheEntry.CacheableFilters);
            }

            var pageContext = new PageContext(context.ActionContext)
            {
                ActionDescriptor = cacheEntry.ActionDescriptor,
                ValueProviderFactories = new CopyOnWriteList<IValueProviderFactory>(_valueProviderFactories),
                ViewData = cacheEntry.ViewDataFactory(_modelMetadataProvider, context.ActionContext.ModelState),
                ViewStartFactories = cacheEntry.ViewStartFactories.ToList(),
            };

            context.Result = new PageActionInvoker(
                _selector,
                _diagnosticSource,
                _logger,
                _mapper,
                pageContext,
                filters,
                cacheEntry,
                _parameterBinder,
                _tempDataFactory,
                _htmlHelperOptions);
        }

        public void OnProvidersExecuted(ActionInvokerProviderContext context)
        {
            throw new NotImplementedException();
        }
    }
}