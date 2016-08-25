using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;

namespace XmlSitemap.Test
{
    [TestClass]
    public class XmlSitemapHandlerTest {
        private Mock<ISitemapCache> _mockCache;
        private Mock<IXmlSitemapGenerator> _mockGenerator;
        private Mock<IContentFilter> _mockFilter;
        private MemoryStream _memoryStream;
        private Mock<HttpContextBase> _httpContext;
        private XDocument _sitemap;
        

        [TestInitialize]
        public void Setup() {
            _mockCache = new Mock<ISitemapCache>();
            _mockGenerator = new Mock<IXmlSitemapGenerator>();
            _mockFilter = new Mock<IContentFilter>();

            _memoryStream = new MemoryStream();
            _httpContext = new Mock<HttpContextBase>();

            var responseMock = new Mock<HttpResponseBase>();
            responseMock.Setup(m => m.OutputStream).Returns(_memoryStream);

            _httpContext.Setup(m => m.Response).Returns(responseMock.Object);

            _sitemap = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("root"));
        }

        [TestCleanup]
        public void Teardown() {
            _memoryStream.Dispose();
        }

        [TestMethod]
        public void TestProcessRequestInCache() {
            _mockCache.Setup(m => m.IsInCache()).Returns(true);
            _mockCache.Setup(m => m.Retrieve()).Returns(_sitemap);
            
            var handler = new XmlSitemapHandler(_mockCache.Object, _mockGenerator.Object, _mockFilter.Object);

            handler.ProcessRequest(_httpContext.Object);

            // Validate that only retrieve was called
            _mockCache.Verify(m => m.IsInCache(), Times.Once);
            _mockCache.Verify(m => m.Retrieve(), Times.Once);
            _mockCache.Verify(m => m.Insert(It.IsAny<XDocument>()), Times.Never);

            // Validate the filter was never called
            _mockFilter.Verify(m => m.GetContent(), Times.Never);

            // Validate generate wasnt called
            _mockGenerator.Verify(m => m.Generate(It.IsAny<IEnumerable<IPublishedContent>>()), Times.Never);
        }

        [TestMethod]
        public void TestProcessRequestOutOfCache() {
            _mockCache.Setup(m => m.IsInCache()).Returns(false);
            _mockFilter.Setup(m => m.GetContent()).Returns(new List<IPublishedContent>());
            _mockGenerator.Setup(m => m.Generate(It.IsAny<IEnumerable<IPublishedContent>>())).Returns(_sitemap);

            var handler = new XmlSitemapHandler(_mockCache.Object, _mockGenerator.Object, _mockFilter.Object);

            handler.ProcessRequest(_httpContext.Object);

            // Validate that only retrieve was called
            _mockCache.Verify(m => m.IsInCache(), Times.Once);
            _mockCache.Verify(m => m.Retrieve(), Times.Never);
            _mockCache.Verify(m => m.Insert(It.IsAny<XDocument>()), Times.Once);

            // Validate the filter was never called
            _mockFilter.Verify(m => m.GetContent(), Times.Once);

            // Validate generate wasnt called
            _mockGenerator.Verify(m => m.Generate(It.IsAny<IEnumerable<IPublishedContent>>()), Times.Once);
        }
    }
}
