//using System.Collections.Concurrent;
//using Microsoft.AspNetCore.Mvc.Abstractions;
//using Microsoft.AspNetCore.Mvc.RazorPages.Internal;
//
//namespace Route
//{
//    internal class InnerCache
//    {
//        public int Version { get; }
//        
//        public InnerCache(int version)
//        {
//            Version = version;
//        }
//
//        public ConcurrentDictionary<ActionDescriptor, PageActionInvokerCacheEntry> Entries { get; } =
//            new ConcurrentDictionary<ActionDescriptor, PageActionInvokerCacheEntry>();
//    }
//}