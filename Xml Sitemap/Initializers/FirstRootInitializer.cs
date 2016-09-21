using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers
{
    /// <summary>
    /// Gets all the content under and including the first root Umbraco node
    /// </summary>
    internal class FirstRootInitializer : BaseUmbracoInitializer, IInitializer {
        public FirstRootInitializer(UmbracoHelper helper) : base(helper) { }

        public IEnumerable<IPublishedContent> GetContent() {
            return Helper.TypedContentAtRoot().First().DescendantsOrSelf();
        }
    }
}
