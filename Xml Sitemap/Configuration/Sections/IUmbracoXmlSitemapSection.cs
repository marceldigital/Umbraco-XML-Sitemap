using System;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Collections;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Sections {
    public interface IUmbracoXmlSitemapSection {
        /// <summary>
        ///     The class to use for caching the sitemap
        /// </summary>
        Type Cache { get; set; }

        /// <summary>
        ///     The class to use to gather the content
        /// </summary>
        Type Engine { get; set; }

        /// <summary>
        ///     The generator to use to format the content
        /// </summary>
        Type Generator { get; set; }

        /// <summary>
        ///     The initializer to use to get the inital content
        /// </summary>
        Type Initializer { get; set; }

        /// <summary>
        ///     List of document types for filters that take a list
        /// </summary>
        FiltersCollection Filters { get; set; }
    }
}