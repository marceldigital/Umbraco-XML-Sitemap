namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    internal static class Constants {
        /// <summary>
        ///     The name of the configuration section of the sitemap
        /// </summary>
        public const string ConfigurationSectionName = "umbracoXmlSitemap";
        /// <summary>
        /// The fully qualified name of the class that will be used to cache
        /// the sitemap
        /// </summary>
        public const string DefaultCachingStrategy = "MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization.HttpContextCache, MarcelDigital.UmbracoExtensions.XmlSitemap";
    }
}