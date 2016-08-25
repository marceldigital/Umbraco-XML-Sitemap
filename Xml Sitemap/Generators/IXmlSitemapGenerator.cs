using System.Collections.Generic;
using System.Xml.Linq;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Generators {
    /// <summary>
    ///     Interface for the generation of a Xml sitemap
    /// </summary>
    public interface IXmlSitemapGenerator {
        /// <summary>
        ///     Creates an xml sitemap from the content
        /// </summary>
        /// <returns></returns>
        XDocument Generate(IEnumerable<IPublishedContent> content);
    }
}