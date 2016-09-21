using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Collections;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Elements;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Sections;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Engines;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace XmlSitemap.Test.Configuration {
    [TestClass]
    public class WebConfigDependencyFactoryTest {
        private const string BaseValidNamespace = "XmlSitemap.Test.Configuration";
        private readonly string _validCacheClass = $"{BaseValidNamespace}.FakeCache";
        private readonly string _validEngineClass = $"{BaseValidNamespace}.FakeEngine";
        private readonly string _validGeneratorClass = $"{BaseValidNamespace}.FakeGenerator";
        private readonly string _validInitializerClass = $"{BaseValidNamespace}.FakeInitialzier";
        private const string InvalidClass = "System.Object";
        private readonly Mock<UmbracoHelper> _mockUmbracoHelper = new Mock<UmbracoHelper>();
        private WebConfigDependencyFactory _sut;

        [TestMethod]
        public void TestValidCache() {
            _sut = new WebConfigDependencyFactory(ValidMock().Object, _mockUmbracoHelper.Object);

            var result = _sut.CreateCache();

            Assert.IsNotNull(result as FakeCache, "An incorrect type was returned");
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException), "The configuration exception was not thrown.")]
        public void TestInvalidCache() {
            _sut = new WebConfigDependencyFactory(InvlaidMock().Object, _mockUmbracoHelper.Object);

            _sut.CreateCache();
        }

        [TestMethod]
        public void TestValidGenerator() {
            _sut = new WebConfigDependencyFactory(ValidMock().Object, _mockUmbracoHelper.Object);

            var result = _sut.CreateGenerator();

            Assert.IsNotNull(result as FakeGenerator, "An incorrect type was returned");
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException), "The configuration exception was not thrown.")]
        public void TestInvalidGenerator() {
            _sut = new WebConfigDependencyFactory(InvlaidMock().Object, _mockUmbracoHelper.Object);

            _sut.CreateGenerator();
        }

        // TODO Create test for engine after IoC
        [TestMethod]
        public void TestCreateEngine() {
            _sut = new WebConfigDependencyFactory(ValidMock().Object, _mockUmbracoHelper.Object);

            _sut.CreateEngine();
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException), "The configuration exception was not thrown.")]
        public void TestInvalidEngine() {
            _sut = new WebConfigDependencyFactory(InvlaidMock().Object, _mockUmbracoHelper.Object);

            _sut.CreateEngine();
        }

        private Mock<IUmbracoXmlSitemapSection> ValidMock()
            => CreateMock(_validEngineClass, _validCacheClass, _validGeneratorClass, _validInitializerClass);

        private Mock<IUmbracoXmlSitemapSection> InvlaidMock()
            => CreateMock(InvalidClass, InvalidClass, InvalidClass, _validInitializerClass+"Bad");

        private Mock<IUmbracoXmlSitemapSection> CreateMock(string engineType, string cacheType, 
                                                            string generatorType, string initializerType) {
            var mock = new Mock<IUmbracoXmlSitemapSection>();

            var filtesrCollection = new FiltersCollection();
            filtesrCollection.Add(new FilterElement {
                Type = typeof(FakeFilter)
            });

            mock.Setup(m => m.Engine).Returns(Type.GetType(engineType));
            mock.Setup(m => m.Cache).Returns(Type.GetType(cacheType));
            mock.Setup(m => m.Generator).Returns(Type.GetType(generatorType));
            mock.Setup(m => m.Initializer).Returns(Type.GetType(initializerType));
            mock.Setup(m => m.Filters).Returns(filtesrCollection);

            return mock;
        }
    }

    internal class FakeFilter : IFilter {
        public IEnumerable<IPublishedContent> Filter(IEnumerable<IPublishedContent> content) {
            throw new NotImplementedException();
        }
    }

    internal class FakeContentEngine : IContentEngine {
        public IEnumerable<IPublishedContent> Run() {
            throw new NotImplementedException();
        }
    }

    internal class FakeGenerator : IXmlSitemapGenerator {
        public XDocument Generate(IEnumerable<IPublishedContent> content) {
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

    internal class FakeInitialzier : IInitializer {
        public FakeInitialzier(UmbracoHelper helper) { }

        public IEnumerable<IPublishedContent> GetContent() {
            throw new NotImplementedException();
        }
    }

    internal class FakeInitialzierBad
    {
        public FakeInitialzierBad(UmbracoHelper helper) { }

        public IEnumerable<IPublishedContent> GetContent() {
            throw new NotImplementedException();
        }
    }
}