using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters on the umbraco nodes by ones with no templates
    /// </summary>
    internal class NoTemplateFilter : UmbracoFilter, IContentFilter {
        public NoTemplateFilter() {}
        public NoTemplateFilter(UmbracoHelper umbracoHelper) : base(umbracoHelper) {}

        public IEnumerable<IPublishedContent> GetContent() {
            var siteRoot = UmbracoHelper.TypedContentAtRoot().First();

            return siteRoot.DescendantsOrSelf().Where(d => d.TemplateId > 0);
        }
    }
}