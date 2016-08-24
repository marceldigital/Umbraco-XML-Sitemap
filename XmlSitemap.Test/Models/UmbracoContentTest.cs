using System;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlSitemap.Test.Models
{
    [TestClass]
    public class UmbracoContentTest
    {
        private TimeSpan _currentMachineOffset;

        [TestInitialize]
        public void Setup() {
            _currentMachineOffset = DateTimeOffset.Now.Offset;
        }

        /// <summary>
        /// Tests that the to XML converter works
        /// </summary>
        [TestMethod]
        public void TestToXml() {
            const string testUrl = "http://test.domain.com/something/";
            var modified = new DateTimeOffset(new DateTime(2016, 1, 12), _currentMachineOffset).DateTime;
            var expected =
                $"<url>\r\n  <loc>{testUrl}</loc>\r\n  <lastmod>{modified.ToString("yyyy-MM-ddTHH:mm:sszzz")}</lastmod>\r\n  <changefreq>weekly</changefreq>\r\n  <priority>0.5</priority>\r\n</url>";

            var url = new UmbracoContent {
                Location = testUrl,
                LastModified = modified
            };

            var xml = url.ToXml();

            Assert.AreEqual(expected, xml.ToString());
        }
    }
}
