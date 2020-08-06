using System.Collections.Generic;
using System.Xml.Linq;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Models {
    /// <summary>
    ///     Representation of the url set node of a XML sitemap.
    /// </summary>
    public class UrlSet : IXmlConvertable {
        /// <summary>
        ///     The xmlns attribute specifies the xml namespace for a document.
        /// </summary>
        public XNamespace XmlNamespace { get; set; }

        /// <summary>
        ///     The xmlns:xhtml attribute specifies the xhtml namespace for a document.
        /// </summary>
        public XNamespace XmlNamespaceXHtml { get; set; }

        /// <summary>
        ///     The child urls of the url set.
        /// </summary>
        public IList<ISitemapContent> Urls { get; set; }

        /// <summary>
        /// Default constructor for the url set.
        /// </summary>
        public UrlSet() {
            XmlNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XmlNamespaceXHtml = "http://www.w3.org/1999/xhtml";
            Urls = new List<ISitemapContent>();
        }

        /// <summary>
        ///     Converts the url set to xml.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml() {
            var urlSetNode = new XElement("urlset",
                new XAttribute("xmlns", XmlNamespace),
                new XAttribute(XNamespace.Xmlns + "xhtml", XmlNamespaceXHtml));

            foreach (var url in Urls) {
                urlSetNode.Add(url.ToXml());
            }

            return urlSetNode;
        }
    }
}