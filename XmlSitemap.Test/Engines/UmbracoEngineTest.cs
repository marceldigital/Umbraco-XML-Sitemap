using System;
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Engines;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Web.BaseRest;

namespace XmlSitemap.Test.Engines
{
    [TestClass]
    public class UmbracoEngineTest {
        private UmbracoEngine _sut;

        [TestInitialize]
        public void Setup() {
            
        }

        [TestCleanup]
        public void Teardown() {
            _sut = null;
        }

        [TestMethod]
        public void TestRun() {
            var mockContent1 = new Mock<IPublishedContent>();
            var mockContent2 = new Mock<IPublishedContent>();
            var mockList = new List<IPublishedContent>();
            mockList.Add(mockContent1.Object);
            mockList.Add(mockContent2.Object);
            var mockInitializer = new Mock<IInitializer>();
            mockInitializer.Setup(m => m.GetContent()).Returns(new List<IPublishedContent>());
            var mockFilter1 = new Mock<IFilter>();
            mockFilter1.Setup(m => m.Filter(It.IsAny<List<IPublishedContent>>())).Returns(new List<IPublishedContent>());
            var mockFilter2 = new Mock<IFilter>();
            mockFilter2.Setup(m => m.Filter(It.IsAny<List<IPublishedContent>>())).Returns(mockList);

            _sut = new UmbracoEngine(mockInitializer.Object, new List<IFilter> {mockFilter1.Object, mockFilter2.Object});

            var result = _sut.Run();

            Assert.AreEqual(2, result.Count());
            mockInitializer.Verify(m => m.GetContent(), Times.Once);
            mockFilter1.Verify(m => m.Filter(It.IsAny<List<IPublishedContent>>()), Times.Once);
            mockFilter2.Verify(m => m.Filter(It.IsAny<List<IPublishedContent>>()), Times.Once);
        }
    }
}
