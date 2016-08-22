using System;
using MarcelDigital.Umbraco.XmlSitemap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XmlSitemap.Test.Models
{
    [TestClass]
    public class UrlSetTest
    {
        //[TestMethod]
        public void TestToXmlBasic() {
            var expected = "";
            var urlSet = new UrlSet();

            var xml = urlSet.ToXml();

            Assert.AreEqual(expected, xml.ToString());
        }

        //[TestMethod]
        public void TestToXmlWithChildren() {
            var expected = "";
            var urlSet = new UrlSet();

            urlSet.Urls.Add(new Url {
                Location = "http://www.domainone.com",
                LastModified = new DateTimeOffset(new DateTime(2016, 1, 12), new TimeSpan(0)).DateTime
            });

            urlSet.Urls.Add(new Url {
                Location = "http://www.domaintwo.com",
                LastModified = new DateTimeOffset(new DateTime(2016, 5, 22), new TimeSpan(0)).DateTime
            });

            var xml = urlSet.ToXml();

            Assert.AreEqual(expected, xml.ToString());
        }
    }
}
