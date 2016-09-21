using System;
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;

namespace XmlSitemap.Test.Filters
{
    [TestClass]
    public class DocumentTypeWhitelistFilterTest
    {
        private DocumentTypeWhitelistFilter _sut;
        private IList<IPublishedContent> _testContent;
        private const string GoodDocumentTypeAlias = "GoodDocType";
        private const string BadDocumentTypeAlias = "BadDocType";

        [TestInitialize]
        public void Setup() {
            _sut = new DocumentTypeWhitelistFilter(new List<string> { GoodDocumentTypeAlias });
            _testContent = new List<IPublishedContent>();

            var content1 = new Mock<IPublishedContent>();
            content1.Setup(c => c.DocumentTypeAlias).Returns(GoodDocumentTypeAlias);
            var content2 = new Mock<IPublishedContent>();
            content2.Setup(c => c.DocumentTypeAlias).Returns(BadDocumentTypeAlias);
            var content3 = new Mock<IPublishedContent>();
            content3.Setup(c => c.DocumentTypeAlias).Returns(GoodDocumentTypeAlias);

            _testContent.Add(content1.Object);
            _testContent.Add(content2.Object);
            _testContent.Add(content3.Object);
        }

        [TestCleanup]
        public void Cleanup() {
            _sut = null;
        }

        [TestMethod]
        public void FilterTest() {
            var filteredContent = _sut.Filter(_testContent);

            Assert.AreEqual(2, filteredContent.Count());
        }
    }
}
