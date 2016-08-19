using System;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace MarcelDigital.Umbraco.XmlSitemap.Optimization {
    /// <summary>
    ///     Http context implementation of the cache.
    /// </summary>
    internal class HttpContextCache : ISitemapCache {
        /// <summary>
        ///     Unique key to put in the cache to identify the sitemap
        /// </summary>
        private static readonly string CacheKey = Guid.NewGuid().ToString();

        /// <summary>
        ///     The current http context
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        ///     Constructor for the cache.
        /// </summary>
        /// <param name="httpContext">The current http context.</param>
        public HttpContextCache(HttpContext httpContext) {
            _httpContext = httpContext;
        }

        /// <summary>
        ///     Checks if the sitemap is in the cache
        /// </summary>
        /// <returns></returns>
        public bool IsInCache()
            => _httpContext.Cache[CacheKey] != null || _httpContext.Cache[CacheKey] is XDocument;

        /// <summary>
        ///     Gets the sitemap from the cache
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public XDocument Retrieve()
            => _httpContext.Cache[CacheKey] as XDocument;

        /// <summary>
        ///     Puts the sitemap in the cache for one day
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sitemap"></param>
        public void Insert(XDocument sitemap)
            => _httpContext.Cache.Insert(CacheKey, sitemap, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
    }
}