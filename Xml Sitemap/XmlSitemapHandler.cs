using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using MarcelDigital.Umbraco.XmlSitemap.Optimization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.Umbraco.XmlSitemap {
    /// <summary>
    ///     Generetes the Xml sitemap for the umbraco website
    /// </summary>
    public class XmlSitemapHandler : IHttpHandler {
        /// <summary>
        ///     Specifies whether the handler can be reused in the pool
        ///     or a new one needs to be created each time
        /// </summary>
        public bool IsReusable => true;

        /// <summary>
        ///     List of document type aliases to exclude from the sitemap
        /// </summary>
        private static readonly string[] DocumentTypeBlacklist = {"eventYear"};

        private readonly ISitemapCache _sitemapCache;

        /// <summary>
        /// Constructor for the sitemap handler
        /// </summary>
        public XmlSitemapHandler() {
            _sitemapCache = new HttpContextCache(HttpContext.Current);
        }

        /// <summary>
        ///     Default method for the http request
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context) {
            UmbracoContext.EnsureContext(
                new HttpContextWrapper(HttpContext.Current),
                ApplicationContext.Current,
                true);

            var sitemap = GenerateSitemapXml(context);

            InsertSitemapIntoResponse(context, sitemap);
        }

        /// <summary>
        ///     Generates the sitemap and inserts it in the response
        /// </summary>
        /// <param name="context"></param>
        private XDocument GenerateSitemapXml(HttpContext context) {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            var siteRoot = umbracoHelper.TypedContentAtRoot().First();
            XDocument sitemap = null;

            if (!_sitemapCache.IsInCache()) {
                var rootNode = GenerateSitemapRoot();

                sitemap = new XDocument {
                    Declaration = new XDeclaration("1.0", "utf-8", "yes")
                };

                sitemap.Add(rootNode);

                AddPagesToSitemap(rootNode, siteRoot);

                _sitemapCache.Insert(sitemap);
            } else {
                sitemap = _sitemapCache.Retrieve();
            }

            return sitemap;
        }

        /// <summary>
        ///     Generates the root of the xml sitemap
        /// </summary>
        /// <returns></returns>
        private static XElement GenerateSitemapRoot() {
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xhtml = "http://www.w3.org/1999/xhtml";

            var root = new XElement("urlset",
                new XAttribute("xmlns", ns),
                new XAttribute(XNamespace.Xmlns + "xhtml", xhtml));

            return root;
        }

        /// <summary>
        ///     Adds the umbraco pages to the sitemap which aren't in the blacklist and have
        ///     a template
        /// </summary>
        private static void AddPagesToSitemap(XElement root, IPublishedContent contentRoot) {
            foreach (
                var content in
                    contentRoot.Descendants()
                               .Where(d => !DocumentTypeBlacklist.Contains(d.DocumentTypeAlias))
                               .Where(d => d.TemplateId > 0)) {
                root.Add(new XElement("url", new XElement("loc", content.UrlWithDomain()),
                    new XElement("lastmod", content.UpdateDate.ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    new XElement("changefreq", "weekly"),
                    new XElement("priority", "0.5")
                    ));
            }
        }

        /// <summary>
        ///     Puts the sitemap into the http response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sitemap"></param>
        private static void InsertSitemapIntoResponse(HttpContext context, XDocument sitemap) {
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