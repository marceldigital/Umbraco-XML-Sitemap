# XML Sitemap Umbraco Plugin
## Installation
`Install-Package MarcelDigital.UmbracoExtensions.XmlSitemap`
## Default Content Filters
The Umbraco XML Sitemap comes with a number of filters out of the box to cover most of the needs of an Umbraco site.
### No Template Filter
This filter will remove all Umbraco nodes from the sitemap which have no display template assigned to them. 

To configure this filter, use the following class in the `web.conf` of the website:
```xml
<umbracoXmlSitemap filter="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.BlacklistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap" ... />
```

### Whitelist Filter
This filter will add all the Umbraco nodes that have a matching document type alias in the list of document type aliases provided
in the sitemap configuration. 

To configure this filter, use the following class in the `web.conf` of the website and add the whitelist of document types:
```xml
<umbracoXmlSitemap filter="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.WhitelistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap" ...>
    <documentTypes>
        <add alias="DocumentTypeAlias1" />
        <add alias="DocumentTypeAlias2" />
    </documentTypes>
</umbracoXmlSitemap>
```

### Blacklist Filter
This filter will remove all the Umbraco nodes that have a matching document type alias in the list of document type aliases provided
in the sitemap configuration. 

To configure this filter, use the following class in the `web.conf` of the website and add the whitelist of document types:
```xml
<umbracoXmlSitemap filter="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.BlacklistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap" ...>
    <documentTypes>
        <add alias="DocumentTypeAlias1" />
        <add alias="DocumentTypeAlias2" />
    </documentTypes>
</umbracoXmlSitemap>
```
## Custom Content Filters
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
<umbracoXmlSitemap filter="MyCool.NameSpace.CustomFilter, MyAssemblyName" ... />
```