using System;
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters the umbraco nodes by removing the nodes which appear
    ///     in the document type list provided by the configuration.
    /// </summary>
    public class BlacklistFilter : UmbracoListFilter, IContentFilter {
        public BlacklistFilter() {}
        public BlacklistFilter(IUmbracoXmlSitemapSection config) : base(config) {}
        public BlacklistFilter(IUmbracoXmlSitemapSection config, UmbracoHelper helper) : base(config, helper) {}

        public IEnumerable<IPublishedContent> GetContent() {
            return UmbracoHelper.TypedContentAtRoot()
                                .First()
                                .DescendantsOrSelf()
                                .Where(c => !DocumentTypeList.Any(w => w.Equals(c.DocumentTypeAlias,
                                    StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}