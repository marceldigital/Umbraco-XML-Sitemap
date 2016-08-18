using System;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;

namespace MarcelDigital.Umbraco.XmlSitemap {
    internal class XmlSitemapCache {
        /// <summary>
        ///     Unique key to put in the cache to identify the sitemap
        /// </summary>
        private static readonly string CacheKey = Guid.NewGuid().ToString();

        /// <summary>
        ///     Checks if the sitemap is in the cache
        /// </summary>
        /// <returns></returns>
        public static bool IsSitemapInCache(HttpContext context)
            => context.Cache[CacheKey] != null || context.Cache[CacheKey] is XDocument;

        /// <summary>
        ///     Gets the sitemap from the cache
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static XDocument GetSitemapFromCache(HttpContext context)
            => context.Cache[CacheKey] as XDocument;

        /// <summary>
        ///     Puts the sitemap in the cache for one day
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sitemap"></param>
        public static void PutSitemapInCache(HttpContext context, XDocument sitemap)
            => context.Cache.Insert(CacheKey, sitemap, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
    }
}