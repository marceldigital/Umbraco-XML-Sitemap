using System;
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters the umbraco nodes by adding the nodes which appear
    ///     in the document type list provided by the configuration.
    /// </summary>
    public class WhitelistFilter : UmbracoListFilter, IContentFilter {
        public WhitelistFilter() {}
        public WhitelistFilter(IUmbracoXmlSitemapSection config) : base(config) {}
        public WhitelistFilter(IUmbracoXmlSitemapSection config, UmbracoHelper helper) : base(config, helper) {}

        public IEnumerable<IPublishedContent> GetContent() {
            return UmbracoHelper.TypedContentAtRoot()
                                .First()
                                .DescendantsOrSelf()
                                .Where(c => DocumentTypeList.Any(w => w.Equals(c.DocumentTypeAlias,
                                    StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}