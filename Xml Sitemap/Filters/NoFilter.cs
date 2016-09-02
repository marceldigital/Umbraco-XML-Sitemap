using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters
{
    /// <summary>
    ///     Applyes no filter to the Umbraco nodes and returns them all
    /// </summary>
    public class NoFilter : UmbracoFilter, IContentFilter
    {
        public NoFilter() { }
        public NoFilter(UmbracoHelper umbracoHelper) : base(umbracoHelper) { }

        public IEnumerable<IPublishedContent> GetContent() {
            return UmbracoHelper.TypedContentAtRoot().DescendantsOrSelf<IPublishedContent>();
        }
    }
}
