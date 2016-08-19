using System.Collections.Generic;
using System.Xml.Linq;
using MarcelDigital.Umbraco.XmlSitemap.Models;
using Umbraco.Core.Models;

namespace MarcelDigital.Umbraco.XmlSitemap.Generators {
    internal class XmlSitemapGenerator : IXmlSitemapGenerator {
        /// <summary>
        ///     Generates a valid xml sitemap from the supplied content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public XDocument Generate(IEnumerable<IPublishedContent> content) {
            var urlSet = new UrlSet();

            foreach (var pc in content) {
                urlSet.Urls.Add(new Url(pc));
            }

            var sitemap = new XDocument {
                Declaration = new XDeclaration("1.0", "utf-8", "yes")
            };

            sitemap.Add(urlSet.ToXml());

            return sitemap;
        }
    }
}