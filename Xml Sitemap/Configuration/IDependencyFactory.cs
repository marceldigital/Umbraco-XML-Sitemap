using MarcelDigital.UmbracoExtensions.XmlSitemap.Engines;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    /// <summary>
    ///     Creates the dependancies for the http handler
    /// </summary>
    public interface IDependencyFactory {
        /// <summary>
        ///     Creates a content gathering engine
        /// </summary>
        /// <returns></returns>
        IContentEngine CreateEngine();

        /// <summary>
        ///     Creates an instance of the sitemap cache
        /// </summary>
        /// <returns></returns>
        ISitemapCache CreateCache();

        /// <summary>
        ///     Creates an instance of the sitemap generator
        /// </summary>
        /// <returns></returns>
        IXmlSitemapGenerator CreateGenerator();
    }
}