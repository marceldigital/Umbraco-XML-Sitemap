using System;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    public interface IUmbracoXmlSitemapSection {
        /// <summary>
        ///     The class to use for caching the sitemap
        /// </summary>
        Type Cache { get; set; }

        /// <summary>
        ///     The filter to use to gather the umbraco content
        /// </summary>
        Type Filter { get; set; }
    }
}