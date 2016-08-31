using System;
using System.ComponentModel;
using System.Configuration;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    public class UmbracoXmlSitemapSection : ConfigurationSection, IUmbracoXmlSitemapSection {
        private const string CacheKey = "cache";
        private const string FilterKey = "filter";

        /// <summary>
        ///     The class to use for caching the sitemap
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("cache", IsRequired = true)]
        public Type Cache {
            get { return this[CacheKey] as Type; }
            set { this[CacheKey] = value; }
        }

        /// <summary>
        ///     The filter to use to gather the umbraco content
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("filter", IsRequired = true)]
        public Type Filter {
            get { return this[FilterKey] as Type; }
            set { this[FilterKey] = value; }
        }
    }
}