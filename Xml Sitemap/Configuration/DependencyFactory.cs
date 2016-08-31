using System;
using System.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    /// <summary>
    ///     Factory to create the dependencies required for the sitemap to be
    ///     generated.
    /// </summary>
    internal class DependencyFactory {
        private readonly IUmbracoXmlSitemapSection _config;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public DependencyFactory() {
            _config = (UmbracoXmlSitemapSection) ConfigurationManager.GetSection("umbracoXmlSitemap");
        }

        /// <summary>
        ///     Test constructor
        /// </summary>
        /// <param name="config">The umbraco xml sitemap configuration</param>
        internal DependencyFactory(IUmbracoXmlSitemapSection config) {
            _config = config;
        }

        /// <summary>
        ///     Creates the filter based on the configuration
        /// </summary>
        /// <returns></returns>
        public IContentFilter CreateFilter() {
            var filter = Activator.CreateInstance(_config.Filter) as IContentFilter;

            if (filter == null) {
                throw new ConfigurationErrorsException("Umbraco XML Sitemap filter has to implement IContentFilter");
            }

            return filter;
        }

        /// <summary>
        ///     Creates the cache based on the configuration
        /// </summary>
        /// <returns></returns>
        public ISitemapCache CreateSitemapCache() {
            var sitemapCache = Activator.CreateInstance(_config.Cache) as ISitemapCache;

            if (sitemapCache == null) {
                throw new ConfigurationErrorsException("Umbraco XML Sitemap cache has to implement ISitemapCache");
            }

            return sitemapCache;
        }
    }
}