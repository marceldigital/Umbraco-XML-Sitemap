using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Engines;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Umbraco.Core;
using Umbraco.Core.Logging;
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
        ///     The engine to use to get the content.
        /// </summary>
        private readonly IContentEngine _contentEngine;

        /// <summary>
        ///     Constructor for the sitemap handler
        /// </summary>
        public XmlSitemapHandler() {
            UmbracoContext.EnsureContext(
                new HttpContextWrapper(HttpContext.Current),
                ApplicationContext.Current,
                true);

            var factory = new WebConfigDependencyFactory();

            _sitemapCache = factory.CreateCache();
            _generator = factory.CreateGenerator();
            _contentEngine = factory.CreateEngine();
        }

        /// <summary>
        ///     Constructor to inject dependencies into the handler
        /// </summary>
        public XmlSitemapHandler(IDependencyFactory dependencyFactory) {
            _sitemapCache = dependencyFactory.CreateCache();
            _generator = dependencyFactory.CreateGenerator();
            _contentEngine = dependencyFactory.CreateEngine();
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
        ///     Alternate method for the http request
        /// </summary>
        /// <param name="context">Current http context</param>
        public void ProcessRequest(HttpContextBase context) {
            LogHelper.Debug<XmlSitemapHandler>("Processing xml sitemap request.");
            try {
                var sitemap = GenerateSitemapXml();

                InsertSitemapIntoResponse(context, sitemap);
            } catch (Exception e) {
                LogHelper.Error<XmlSitemapHandler>($"An error occured returning the sitemap: {e.Message}", e);
                throw;
            }
            LogHelper.Debug<XmlSitemapHandler>("Completed xml sitemap request.");
        }

        /// <summary>
        ///     Generates the sitemap and inserts it in the response
        /// </summary>
        private XDocument GenerateSitemapXml() {
            XDocument sitemap;

            if (!_sitemapCache.IsInCache()) {
                LogHelper.Debug<XmlSitemapHandler>("Xml sitemap found is not in the cache, generating...");
                var content = _contentEngine.Run();

                sitemap = _generator.Generate(content);

                _sitemapCache.Insert(sitemap);
            } else {
                LogHelper.Debug<XmlSitemapHandler>("Xml sitemap is in the cache, retrieving...");
                sitemap = _sitemapCache.Retrieve();
            }

            return sitemap;
        }

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
            response.Flush();
        }
    }
}