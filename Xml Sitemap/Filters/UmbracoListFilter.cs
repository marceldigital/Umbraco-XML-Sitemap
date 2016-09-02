using System.Collections.Generic;
using System.Configuration;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Configuration;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    /// <summary>
    ///     Abstract class to retireve the list configuration from the configuration
    ///     and make it available to sub types.
    /// </summary>
    public abstract class UmbracoListFilter : UmbracoFilter {
        protected readonly IList<string> DocumentTypeList;

        protected UmbracoListFilter()
            : this((UmbracoXmlSitemapSection) ConfigurationManager.GetSection(Constants.ConfigurationSectionName)) {}

        protected UmbracoListFilter(IUmbracoXmlSitemapSection config)
            : this(config, new UmbracoHelper(UmbracoContext.Current)) {}

        protected UmbracoListFilter(IUmbracoXmlSitemapSection config, UmbracoHelper helper) : base(helper) {
            DocumentTypeList = new List<string>();
            foreach (var configDocumentTypeObj in config.DocumentTypes) {
                var configDocumentType = configDocumentTypeObj as DocumentTypeElement;
                if (configDocumentType != null) {
                    DocumentTypeList.Add(configDocumentType.Alias);
                }
            }
        }
    }
}