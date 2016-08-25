using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap {
    /// <summary>
    ///     Generetes the Xml sitemap for the umbraco website.
    /// </summary>
    public class XmlSitemapHandler : IHttpHandler {
        /// <summary>
        ///     Specifies whether the handler can be reused in the pool
        ///     or a new one needs to be created each time.
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        ///     Caching strategy for the XML sitemap.
        /// </summary>
        private readonly ISitemapCache _sitemapCache;

        /// <summary>
        ///     Generation strategy for the XML sitemap.
        /// </summary>
        private readonly IXmlSitemapGenerator _generator;

        /// <summary>
        ///     The filter to use on the Umbraco content.
        /// </summary>
        private readonly IContentFilter _filter;

        /// <summary>
        ///     Constructor for the sitemap handler
        /// </summary>
        public XmlSitemapHandler() {
            UmbracoContext.EnsureContext(
                new HttpContextWrapper(HttpContext.Current),
                ApplicationContext.Current,
                true);

            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            _sitemapCache = new HttpContextCache(HttpContext.Current);
            _generator = new XmlSitemapGenerator();
            _filter = new NoTemplateFilter(umbracoHelper);
        }

        /// <summary>
        /// Constructor to inject dependanices into the handler
        /// </summary>
        /// <param name="cacheStrategy">Cache strategy of the handler for the sitemap.</param>
        /// <param name="generator">Generator for the sitemap.</param>
        /// <param name="filter">Filter for the umbraco content.</param>
        public XmlSitemapHandler(ISitemapCache cacheStrategy, IXmlSitemapGenerator generator, IContentFilter filter) {
            _sitemapCache = cacheStrategy;
            _generator = generator;
            _filter = filter;
        }

        /// <summary>
        ///     Default method for the http request
        /// </summary>
        /// <param name="context">Current http context</param>
        public void ProcessRequest(HttpContext context) {
            HttpContextBase wrapper = new HttpContextWrapper(context);
            ProcessRequest(wrapper);
        }

        /// <summary>
        /// Alternate method for the http request
        /// </summary>
        /// <param name="context">Current http context</param>
        public void ProcessRequest(HttpContextBase context) {
            var sitemap = GenerateSitemapXml();

            InsertSitemapIntoResponse(context, sitemap);
        }

        /// <summary>
        ///     Generates the sitemap and inserts it in the response
        /// </summary>
        private XDocument GenerateSitemapXml() {
            XDocument sitemap;

            if (!_sitemapCache.IsInCache()) {
                var content = GetContent();

                sitemap = _generator.Generate(content);

                _sitemapCache.Insert(sitemap);
            } else {
                sitemap = _sitemapCache.Retrieve();
            }

            return sitemap;
        }

        /// <summary>
        ///     Gets the Umbraco content to submit to the sitemap XML
        /// </summary>
        protected virtual IEnumerable<IPublishedContent> GetContent()
            => _filter.GetContent();

        /// <summary>
        ///     Puts the sitemap into the http response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sitemap"></param>
        private static void InsertSitemapIntoResponse(HttpContextBase context, XDocument sitemap) {
            var response = context.Response;
            response.Clear();
            response.ContentType = "text/xml";

            using (var streamWriter = new StreamWriter(response.OutputStream, Encoding.UTF8)) {
                var xmlWriter = new XmlTextWriter(streamWriter);
                sitemap.WriteTo(xmlWriter);
            }
            response.End();
        }
    }
}