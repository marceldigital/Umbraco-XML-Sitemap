using System.Xml.Linq;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization {
    public interface ISitemapCache {
        /// <summary>
        ///     Checks if the sitemap is in the cache
        /// </summary>
        /// <returns></returns>
        bool IsInCache();

        /// <summary>
        ///     Gets the sitemap from the cache
        /// </summary>
        /// <returns></returns>
        XDocument Retrieve();

        /// <summary>
        ///     Puts the sitemap in the cache for one day
        /// </summary>
        /// <param name="sitemap"></param>
        void Insert(XDocument sitemap);
    }
}