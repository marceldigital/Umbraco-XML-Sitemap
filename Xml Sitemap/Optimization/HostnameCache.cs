using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization
{
    public class HostnameCache : ISitemapCache
    {
        /// <summary>
        ///     Unique key to put in the cache to identify the sitemap
        /// </summary>
        private static readonly string BaseCacheKey = Guid.NewGuid().ToString();

        private readonly string CacheKey;

        /// <summary>
        ///     The current http context
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        ///     Default constructor for the cache.
        /// </summary>
        public HostnameCache() : this(HttpContext.Current) { }

        /// <summary>
        ///     Constructor for the cache.
        /// </summary>
        /// <param name="httpContext">The current http context.</param>
        public HostnameCache(HttpContext httpContext)
        {
            _httpContext = httpContext;
            CacheKey = $"{BaseCacheKey}-{_httpContext?.Request?.Url?.Host ?? "unkown"}";
        }

        /// <summary>
        ///     Checks if the sitemap is in the cache
        /// </summary>
        /// <returns></returns>
        public bool IsInCache()
            => (_httpContext.Cache[CacheKey] != null) || _httpContext.Cache[CacheKey] is XDocument;

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
