using System;
using System.ComponentModel;
using System.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Collections;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Sections {
    public class UmbracoXmlSitemapSection : ConfigurationSection, IUmbracoXmlSitemapSection {
        private const string CacheKey = "cache";
        private const string EngineKey = "filter";
        private const string GeneratorKey = "generator";
        private const string FiltersKey = "filters";
        private const string InitializerKey = "initializer";

        /// <summary>
        ///     The class to use for caching the sitemap
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(CacheKey, IsRequired = false , DefaultValue = Constants.DefaultCachingStrategy)]
        public Type Cache {
            get { return this[CacheKey] as Type; }
            set { this[CacheKey] = value; }
        }

        /// <summary>
        ///     The engine to use to gather and filter the umbraco content
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(EngineKey, IsRequired = false, DefaultValue = Constants.DefaultEngine)]
        public Type Engine {
            get { return this[EngineKey] as Type; }
            set { this[EngineKey] = value; }
        }

        /// <summary>
        ///     The initializer to use to gather the initial umbraco content
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(InitializerKey, IsRequired = false, DefaultValue = Constants.DefaultInitializer)]
        public Type Initializer {
            get { return this[InitializerKey] as Type; }
            set { this[InitializerKey] = value; }
        }

        /// <summary>
        ///     The engine to use to gather the umbraco content
        /// </summary>
        [TypeConverter(typeof(TypeNameConverter))]
        [ConfigurationProperty(GeneratorKey, IsRequired = false, DefaultValue = Constants.DefaultGenerator)]
        public Type Generator {
            get { return this[GeneratorKey] as Type; }
            set { this[GeneratorKey] = value; }
        }

        /// <summary>
        ///     List of document types for filters that take a list
        /// </summary>
        [ConfigurationProperty(FiltersKey, IsDefaultCollection = false, IsRequired = true)]
        [ConfigurationCollection(typeof(FiltersCollection), 
            AddItemName = "filter",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public FiltersCollection Filters {
            get { return this[FiltersKey] as FiltersCollection; }
            set { this[FiltersKey] = value; }
        }
    }
}