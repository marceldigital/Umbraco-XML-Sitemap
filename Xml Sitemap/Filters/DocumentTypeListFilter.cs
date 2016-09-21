using System.Collections.Generic;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Abstract class to retireve the list configuration from the configuration
    ///     and make it available to sub types.
    /// </summary>
    public abstract class DocumentTypeListFilter {
        protected readonly IList<string> DocumentTypeList;

        protected DocumentTypeListFilter(IList<string> documentTypeList) {
            DocumentTypeList = documentTypeList;
        }
    }
}