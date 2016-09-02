using System.Configuration;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    /// <summary>
    ///     Element to represent a document type in document list
    /// </summary>
    public class DocumentTypeElement : ConfigurationElement {
        private const string AliasKey = "alias";

        /// <summary>
        ///     Alias of the documnet type
        /// </summary>
        [ConfigurationProperty(AliasKey, IsRequired = true, IsKey = true)]
        public string Alias {
            get { return this[AliasKey] as string; }
            set { this[AliasKey] = value; }
        }
    }
}