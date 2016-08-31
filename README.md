# XML Sitemap Umbraco Plugin
## Installation
`Install-Package MarcelDigital.UmbracoExtensions.XmlSitemap`
## Custom Content Gathering
The sitemap generator can also be used with a custom filter in order to get the Umbraco nodes that you want
the sitemap to display. To do this, extend the `UmbracoFilter` class (which will give you access to an Umbraco helper)
and implement the `IContentFilter` interface so that the generator will be able to gather your content.

Example:

```csharp
using System.Collections.Generic;
using System.Linq;
using MarcelDigital.UmbracoExtensions.XmlSitemap.Filters;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace MyCool.NameSpace
{
    // Filter to gather all nodes level 2 and below
    public class CustomFilter : UmbracoFilter, IContentFilter
    {
        public IEnumerable<IPublishedContent> GetContent() {
            return UmbracoHelper.TypedContentAtRoot()
                                .First()
                                .DescendantsOrSelf()
                                .Where(c => c.Level <= 2);
        }
    }
}
```

Then update the `<umbracoXmlSitemap/>` node in the `web.config`. Set the `filter` attribute to
the custom filter class.
    
Example:

```xml
<umbracoXmlSitemap cache="MarcelDigital.UmbracoExtensions.XmlSitemap.Optimization.HttpContextCache, MarcelDigital.UmbracoExtensions.XmlSitemap" 
                   filter="MyCool.NameSpace.CustomFilter, MyAssemblyName" />
```