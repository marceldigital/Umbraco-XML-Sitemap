using System;
using System.Xml.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Models {
    internal class UmbracoContent : ISitemapContent {
        /// <summary>
        ///     Provides the full URL of the page or sitemap, including the protocol (e.g. http, https) and a trailing slash.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     The date that the file was last modified, in ISO 8601 format.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        ///     How frequently the page may change.
        /// </summary>
        public string ChangeFrequency { get; set; }

        /// <summary>
        ///     The priority of that URL relative to other URLs on the site.
        /// </summary>
        public double Priority { get; set; }

        /// <summary>
        ///     Default constructor for the url node.
        /// </summary>
        public UmbracoContent() {
            ChangeFrequency = "weekly";
            Priority = 0.5;
        }

        /// <summary>
        ///     Converts the Url to an xml node.
        /// </summary>
        /// <returns></returns>
        public XElement ToXml() {
            return new XElement("url", new XElement("loc", Location),
                new XElement("lastmod", LastModified.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                new XElement("changefreq", ChangeFrequency),
                new XElement("priority", Priority.ToString("N1"))
                );
        }

        /// <summary>
        /// Parses a IPublishedContent node into a UmbracoContent
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static UmbracoContent Parse(IPublishedContent content) {
            return new UmbracoContent {
                Location = content.UrlWithDomain(),
                LastModified = content.UpdateDate
            };
        }
    }
}