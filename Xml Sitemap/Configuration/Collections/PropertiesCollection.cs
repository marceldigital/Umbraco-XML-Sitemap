using System.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Elements;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Collections {
    /// <summary>
    ///     Configuration element collection to hold hte list of properties to filter on
    /// </summary>
    public class PropertiesCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() {
            return new PropertyElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((PropertyElement) element).Alias;
        }
    }
}