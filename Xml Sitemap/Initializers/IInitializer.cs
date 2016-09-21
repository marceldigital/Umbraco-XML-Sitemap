using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers {
    /// <summary>
    ///     Interface for classes that will initialize the Umbraco nodes to filter
    ///     on in the sitemap.
    /// </summary>
    public interface IInitializer {
        /// <summary>
        ///     Gets the content from Umbraco to be filtered on
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPublishedContent> GetContent();
    }
}