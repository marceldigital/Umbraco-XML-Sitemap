using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Engines
{
    internal class UmbracoEngine : IContentEngine {
        private readonly IInitializer _initializer;
        private readonly IEnumerable<IFilter> _contentFilters;

        public UmbracoEngine(IInitializer initializer, IEnumerable<IFilter> contentFilters) {
            _initializer = initializer;
            _contentFilters = contentFilters;
        }

        public IEnumerable<IPublishedContent> Run() {
            var content = _initializer.GetContent();

            return _contentFilters.Aggregate(content, (current, contentFilter) => contentFilter.Filter(current));
        }
    }
}
