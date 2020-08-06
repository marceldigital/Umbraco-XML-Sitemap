using System.Xml.Linq;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Models {
    public interface IXmlConvertable {
        /// <summary>
        ///     Converts the object to an XML element
        /// </summary>
        /// <returns></returns>
        XElement ToXml();
    }
}