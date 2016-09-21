using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers
{
    /// <summary>
    /// Gets all the content in Umbraco
    /// </summary>
    internal class AllRootInitializer : BaseUmbracoInitializer, IInitializer
    {
        public AllRootInitializer(UmbracoHelper helper) : base(helper) { }

        public IEnumerable<IPublishedContent> GetContent() {
            return Helper.TypedContentAtRoot().SelectMany(c => c.DescendantsOrSelf());
        }
    }
}
