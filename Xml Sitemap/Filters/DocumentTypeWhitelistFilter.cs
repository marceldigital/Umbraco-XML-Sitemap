using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Filters the umbraco nodes by adding the nodes which appear
    ///     in the document type list provided by the configuration.
    /// </summary>
    public class DocumentTypeWhitelistFilter : DocumentTypeListFilter, IFilter {
        public DocumentTypeWhitelistFilter(IList<string> documentTypeList) : base(documentTypeList) {}

        public IEnumerable<IPublishedContent> Filter(IEnumerable<IPublishedContent> content) {
            return content.Where(c => DocumentTypeList.Any(w => w.Equals(c.DocumentTypeAlias,
                                                                    StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}