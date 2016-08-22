using System;
using System.Collections.Generic;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace XmlSitemap.Test.Generators
{
    [TestClass]
    public class XmlSitemapGeneratorTest
    {
        //[TestMethod]
        public void TestGenerate() {
            var expected = "";
            var mockContent = new Mock<IPublishedContent>();
            //mockContent.Setup(m => m.UrlWithDomain()).Returns("http://www.google.com");
            mockContent.Setup(m => m.UpdateDate).Returns(new DateTimeOffset(new DateTime(2016, 1, 12), new TimeSpan(0)).DateTime);

            var mockContent2 = new Mock<IPublishedContent>();
            //mockContent2.Setup(m => m.UrlWithDomain()).Returns("http://www.amazon.com");
            mockContent2.Setup(m => m.UpdateDate).Returns(new DateTimeOffset(new DateTime(2016, 7, 8), new TimeSpan(0)).DateTime);

            var list = new List<IPublishedContent>();
            list.Add(mockContent.Object);
            list.Add(mockContent2.Object);

            var generator = new XmlSitemapGenerator();
            var sitemap = generator.Generate(list);

            Assert.AreEqual(expected, sitemap.ToString());
        }
    }
}
