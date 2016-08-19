using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MarcelDigital.Umbraco.XmlSitemap.Filters {
    internal interface IContentFilter {
        /// <summary>
        ///     Gets the content that should be used in the XML sitemap
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPublishedContent> GetContent();
    }
}