using System;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlSitemap.Test.Models
{
    [TestClass]
    public class UrlTest
    {
        /// <summary>
        /// Tests that the to XML converter works
        /// </summary>
        [TestMethod]
        public void TestToXml() {
            const string expected = "<url>\r\n  <loc>http://www.google.com</loc>\r\n  <lastmod>2016-01-12T00:00:00-06:00</lastmod>\r\n  <changefreq>weekly</changefreq>\r\n  <priority>0.5</priority>\r\n</url>";

            var url = new Url {
                Location = "http://www.google.com",
                LastModified = new DateTimeOffset(new DateTime(2016, 1, 12), new TimeSpan(0)).DateTime
            };

            var xml = url.ToXml();

            Assert.AreEqual(expected, xml.ToString());
        }
    }
}
