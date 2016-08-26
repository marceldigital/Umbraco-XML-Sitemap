using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace XmlSitemap.Test.Generators {
    [TestClass]
    public class XmlSitemapGeneratorTest {
        private IList<ISitemapContent> _mockContent;

        [TestInitialize]
        public void Setup() {
            var mockContent = new Mock<ISitemapContent>();
            mockContent.Setup(m => m.ToXml()).Returns(new XElement("url"));

            var mockContent2 = new Mock<ISitemapContent>();
            mockContent2.Setup(m => m.ToXml()).Returns(new XElement("url"));

            _mockContent = new List<ISitemapContent> {
                mockContent.Object,
                mockContent2.Object
            };
        }

        [TestMethod]
        public void TestDeclaration() {
            var generator = new XmlSitemapGenerator();
            var sitemap = generator.Generate(_mockContent);

            Assert.AreEqual("1.0", sitemap.Declaration.Version, "the sitemap version is incorrect.");
            Assert.AreEqual("utf-8", sitemap.Declaration.Encoding, "the sitemap version is incorrect.");
            Assert.AreEqual("yes", sitemap.Declaration.Standalone, "the sitemap should be standalone.");
        }

        [TestMethod]
        public void TestContents() {
            var generator = new XmlSitemapGenerator();
            var sitemap = generator.Generate(_mockContent);

            Assert.AreEqual("urlset", sitemap.Root.Name, "urlset is not the root element");
            Assert.AreEqual(2, sitemap.Root.Elements().Count(), "the sitmap has an incorrect number of urls");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException), "Null content was allowed.")]
        public void TestNullContent() {
            IEnumerable<ISitemapContent> nullContent = null;
            var generator = new XmlSitemapGenerator();
            
            generator.Generate(nullContent);
        }
    }
}