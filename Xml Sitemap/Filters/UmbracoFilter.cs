using Umbraco.Web;

namespace MarcelDigital.Umbraco.XmlSitemap.Filters {
    internal abstract class UmbracoFilter {
        /// <summary>
        ///     The umbraco helper for the current session.
        /// </summary>
        protected readonly UmbracoHelper UmbracoHelper;

        /// <summary>
        ///     Default constructor for the Umbraco filter.
        /// </summary>
        /// <param name="umbracoHelper"></param>
        protected UmbracoFilter(UmbracoHelper umbracoHelper) {
            UmbracoHelper = umbracoHelper;
        }
    }
}