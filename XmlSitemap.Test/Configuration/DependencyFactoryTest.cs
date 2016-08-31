using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;

namespace XmlSitemap.Test.Configuration {
    [TestClass]
    public class DependencyFactoryTest {
        private const string BaseValidNamespace = "XmlSitemap.Test.Configuration";
        private readonly string _validFilterClass = $"{BaseValidNamespace}.FakeFilter";
        private readonly string _validCacheClass = $"{BaseValidNamespace}.FakeCache";
        private const string InvalidClass = "System.Object";
        private DependencyFactory _sut;


        [TestMethod]
        public void TestValidFilter() {
            _sut = new DependencyFactory(ValidMock().Object);

            var result = _sut.CreateFilter();

            Assert.IsNotNull(result as FakeFilter, "An incorrect type was returned");
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException), "The configuration exception was not thrown.")]
        public void TestInvalidFilter() {
            _sut = new DependencyFactory(InvlaidMock().Object);

            _sut.CreateFilter();
        }

        [TestMethod]
        public void TestValidCache() {
            _sut = new DependencyFactory(ValidMock().Object);

            var result = _sut.CreateSitemapCache();

            Assert.IsNotNull(result as FakeCache, "An incorrect type was returned");
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException), "The configuration exception was not thrown.")]
        public void TestInvalidCache() {
            _sut = new DependencyFactory(InvlaidMock().Object);

            _sut.CreateSitemapCache();
        }

        private Mock<IUmbracoXmlSitemapSection> ValidMock()
            => CreateMock(_validFilterClass, _validCacheClass);

        private Mock<IUmbracoXmlSitemapSection> InvlaidMock()
            => CreateMock(InvalidClass, InvalidClass);

        private Mock<IUmbracoXmlSitemapSection> CreateMock(string filterType, string cacheType) {
            var mock = new Mock<IUmbracoXmlSitemapSection>();

            mock.Setup(m => m.Filter).Returns(Type.GetType(filterType));
            mock.Setup(m => m.Cache).Returns(Type.GetType(cacheType));

            return mock;
        }
    }

    internal class FakeFilter : IContentFilter {
        public IEnumerable<IPublishedContent> GetContent() {
            throw new NotImplementedException();
        }
    }

    internal class FakeCache : ISitemapCache {
        public bool IsInCache() {
            throw new NotImplementedException();
        }

        public XDocument Retrieve() {
            throw new NotImplementedException();
        }

        public void Insert(XDocument sitemap) {
            throw new NotImplementedException();
        }
    }
}