using System;
using System.ComponentModel;
using System.Configuration;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    public class UmbracoXmlSitemapSection : ConfigurationSection, IUmbracoXmlSitemapSection {
        private const string CacheKey = "cache";
        private const string FilterKey = "filter";
        private const string DocumentTypesKey = "documentTypes";

        /// <summary>
        ///     The class to use for caching the sitemap
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty("cache", IsRequired = false , DefaultValue = Constants.DefaultCachingStrategy)]
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

        /// <summary>
        ///     List of document types for filters that take a list
        /// </summary>
        [ConfigurationProperty("documentTypes", IsDefaultCollection = false, IsRequired = false)]
        [ConfigurationCollection(typeof(DocumentTypesCollection), 
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public DocumentTypesCollection DocumentTypes {
            get { return this[DocumentTypesKey] as DocumentTypesCollection; }
            set { this[DocumentTypesKey] = value; }
        }
    }
}