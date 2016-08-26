using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Models;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Generators {
    internal class XmlSitemapGenerator : IXmlSitemapGenerator {
        /// <summary>
        ///     The version of the XML, usually "1.0".
        /// </summary>
        private const string Version = "1.0";

        /// <summary>
        ///     The encoding for the XML document.
        /// </summary>
        private const string Encoding = "utf-8";

        /// <summary>
        ///     Specifies whether the XML is standalone or required external
        ///     dependencies to be resolved.
        /// </summary>
        private const string Standalone = "yes";

        /// <summary>
        ///     Generates a valid xml sitemap from the supplied content.
        /// </summary>
        /// <param name="content">Content to be added to the sitemap.</param>
        /// <returns>An XML Sitemap.</returns>
        public XDocument Generate(IEnumerable<ISitemapContent> content) {
            if (content == null) throw new ArgumentNullException(nameof(content));

            var urlSet = new UrlSet {
                Urls = content.ToList()
            };

            var sitemap = new XDocument {
                Declaration = new XDeclaration(Version, Encoding, Standalone)
            };

            sitemap.Add(urlSet.ToXml());

            return sitemap;
        }

        /// <summary>
        ///     Generates a valid xml sitemap from the supplied published
        ///     umbraco content.
        /// </summary>
        /// <param name="umbracoContent">The umbraco content</param>
        /// <returns>An XML Sitemap.</returns>
        public XDocument Generate(IEnumerable<IPublishedContent> umbracoContent) {
            var sitemapContent = umbracoContent.Select(UmbracoContent.Parse).ToList();

            return Generate(sitemapContent);
        }
    }
}