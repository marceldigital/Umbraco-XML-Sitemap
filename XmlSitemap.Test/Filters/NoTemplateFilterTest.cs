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
    public class NoTemplateFilterTest {
        private NoTemplateFilter _sut;
        private IList<IPublishedContent> _testContent;

        [TestInitialize]
        public void Setup() {
            _sut = new NoTemplateFilter();
            _testContent = new List<IPublishedContent>();

            var content1 = new Mock<IPublishedContent>();
            content1.Setup(c => c.TemplateId).Returns(111);
            var content2 = new Mock<IPublishedContent>();
            content2.Setup(c => c.TemplateId).Returns(222);
            var content3 = new Mock<IPublishedContent>();
            content3.Setup(c => c.TemplateId).Returns(0);

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

            Assert.AreEqual(filteredContent.Count(), 2);
        }
    }
}
