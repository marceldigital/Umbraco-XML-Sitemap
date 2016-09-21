using System.Collections.Generic;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Engines {
    /// <summary>
    ///     Gets the content for the sitemap
    /// </summary>
    public interface IContentEngine {
        IEnumerable<IPublishedContent> Run();
    }
}