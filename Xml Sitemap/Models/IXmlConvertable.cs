using System.Xml.Linq;

namespace MarcelDigital.Umbraco.XmlSitemap.Models {
    internal interface IXmlConvertable {
        /// <summary>
        ///     Converts the object to an XML element
        /// </summary>
        /// <returns></returns>
        XElement ToXml();
    }
}