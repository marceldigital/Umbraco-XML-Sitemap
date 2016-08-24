using System;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Models {
    internal interface ISitemapContent : IXmlConvertable {
        /// <summary>
        ///     Provides the full URL of the page or sitemap, including the protocol (e.g. http, https) and a trailing slash.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        ///     The date that the file was last modified, in ISO 8601 format.
        /// </summary>
        DateTime LastModified { get; set; }

        /// <summary>
        ///     How frequently the page may change.
        /// </summary>
        string ChangeFrequency { get; set; }

        /// <summary>
        ///     The priority of that URL relative to other URLs on the site.
        /// </summary>
        double Priority { get; set; }

    }
}