using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Filters {
    public abstract class UmbracoFilter {
        /// <summary>
        ///     The umbraco helper for the current session.
        /// </summary>
        protected readonly UmbracoHelper UmbracoHelper;

        /// <summary>
        ///     Default constructor for the Umbraco filter.
        /// </summary>
        protected UmbracoFilter() {
            UmbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        /// <summary>
        ///     Constructor for the Umbraco filter with passed dependency
        /// </summary>
        /// <param name="umbracoHelper"></param>
        protected UmbracoFilter(UmbracoHelper umbracoHelper) {
            UmbracoHelper = umbracoHelper;
        }
    }
}