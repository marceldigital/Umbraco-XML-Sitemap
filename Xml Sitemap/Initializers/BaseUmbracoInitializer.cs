using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers
{
    public abstract class BaseUmbracoInitializer {
        protected readonly UmbracoHelper Helper;

        protected BaseUmbracoInitializer(UmbracoHelper helper) {
            Helper = helper;
        }
    }
}
