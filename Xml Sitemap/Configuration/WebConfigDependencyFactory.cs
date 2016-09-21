using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Elements;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration.Sections;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Engines;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Generators;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration {
    /// <summary>
    ///     Dependancy factory that uses the web config to get its configuration
    /// </summary>
    internal class WebConfigDependencyFactory : IDependencyFactory {
        /// <summary>
        ///     The xml sitemap configuration section
        /// </summary>
        private readonly IUmbracoXmlSitemapSection _config;

        /// <summary>
        ///     The umbraco helper to use to bootstrap the engine
        /// </summary>
        private readonly UmbracoHelper _umbracoHelper;

        internal WebConfigDependencyFactory()
            : this(
                (UmbracoXmlSitemapSection) ConfigurationManager.GetSection(Constants.ConfigurationSectionName),
                new UmbracoHelper(UmbracoContext.Current)) {}

        internal WebConfigDependencyFactory(IUmbracoXmlSitemapSection config, UmbracoHelper umbracoHelper) {
            _config = config;
            _umbracoHelper = umbracoHelper;
        }

        public IContentEngine CreateEngine() {
            var initializer = CreateInitializer();

            //TODO Actually make IoC
            var filters = CreateFilters(_config.Filters);
            var engine = new UmbracoEngine(initializer, filters);

            return engine;
        }

        public ISitemapCache CreateCache() {
            var sitemapCache = Activator.CreateInstance(_config.Cache) as ISitemapCache;

            if (sitemapCache == null) {
                throw new ConfigurationErrorsException("Umbraco XML Sitemap cache has to implement ISitemapCache");
            }

            return sitemapCache;
        }

        public IXmlSitemapGenerator CreateGenerator() {
            var generator = Activator.CreateInstance(_config.Generator) as IXmlSitemapGenerator;

            if (generator == null) {
                throw new ConfigurationErrorsException("Umbraco XML Sitemap cache has to implement ISitemapCache");
            }

            return generator;
        }

        /// <summary>
        /// Creates a initialzier type based on the configuration
        /// </summary>
        /// <param name="initializerType"></param>
        /// <returns></returns>
        private IInitializer CreateInitializer() {
            var initializer = Activator.CreateInstance(_config.Initializer, _umbracoHelper) as IInitializer;

            if (initializer == null) {
                throw new ConfigurationErrorsException("Umbraco XML Sitemap initializer has to implement IInitializer");
            }

            return initializer;
        }

        /// <summary>
        ///     Creates the filters based on the types
        /// </summary>
        /// <param name="filtersCollection"></param>
        private static IList<IFilter> CreateFilters(IEnumerable filtersCollection) {
            var filters = new List<IFilter>();
            foreach (var filterElementObj in filtersCollection) {
                IFilter filter;
                var filterElement = filterElementObj as FilterElement;

                if (filterElement == null) continue;

                var filterType = filterElement.Type;
                if (filterType.IsSubclassOf(typeof(DocumentTypeListFilter))) {
                    var documentTypeList = CreateDocumentTypeList(filterElement);
                    filter = Activator.CreateInstance(filterType, documentTypeList) as IFilter;
                } else {
                    filter = Activator.CreateInstance(filterType) as IFilter;
                }

                if (filter == null) {
                    throw new ConfigurationErrorsException($"The filter {filterType.Name} has to implement IFilter");
                }

                filters.Add(filter);
            }

            return filters;
        }

        private static IList<string> CreateDocumentTypeList(FilterElement filterElement) {
            return filterElement.DocumentTypes
                                .OfType<DocumentTypeElement>()
                                .Select(configDocumentType => configDocumentType.Alias)
                                .ToList();
        }
    }
}