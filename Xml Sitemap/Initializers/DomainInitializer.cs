using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers
{
    public class DomainInitializer : BaseUmbracoInitializer, IInitializer
    {
        public DomainInitializer(UmbracoHelper helper) : base(helper) { }

        public IEnumerable<IPublishedContent> GetContent()
        {
            var requestedHostname = Helper?.UmbracoContext?.HttpContext?.Request?.Url?.Host ?? "";
            var umbracoDomain = Helper?.UmbracoContext?.Application?.Services?.DomainService?.GetByName(requestedHostname);

            if (umbracoDomain == null) return new List<IPublishedContent>();

            var content = Helper.TypedContent(umbracoDomain.RootContentId);

            return content?.DescendantsOrSelf() ?? new List<IPublishedContent>();
        }
    }
}
