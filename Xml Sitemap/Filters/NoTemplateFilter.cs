using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters on the umbraco nodes by ones with no templates
    /// </summary>
    public class NoTemplateFilter : IFilter {
        public IEnumerable<IPublishedContent> Filter(IEnumerable<IPublishedContent> content) {
            return content.Where(d => d.TemplateId > 0);
        }
    }
}