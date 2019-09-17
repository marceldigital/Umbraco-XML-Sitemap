# XML Sitemap Umbraco Plugin
## Installation
`Install-Package MarcelDigital.UmbracoExtensions.XmlSitemap`
## Default Content Filters
The Umbraco XML Sitemap comes with a number of filters out of the box to cover most of the needs of an Umbraco site.
### No Template Filter
This filter will remove all Umbraco nodes from the sitemap which have no display template assigned to them. 

To configure this filter, use the following class in the `xmlSitemap.config` of the website:
```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.NoTemplateFilter, MarcelDigital.UmbracoExtensions.XmlSitemap" />
  </filters>
</umbracoXmlSitemap>
```

### Document Type Whitelist Filter
This filter will add all the Umbraco nodes that have a matching document type alias in the list of document type aliases provided
in the sitemap configuration. 

To configure this filter, use the following class in the `xmlSitemap.config` of the website and add the whitelist of document types:
```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.DocumentTypeWhitelistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap">
        <documentTypes>
            <add alias="DocumentTypeAlias1" />
            <add alias="DocumentTypeAlias2" />
        </documentTypes>
    </filter>
  </filters>
</umbracoXmlSitemap>
```

### Document Type Blacklist Filter
This filter will remove all the Umbraco nodes that have a matching document type alias in the list of document type aliases provided
in the sitemap configuration. 

To configure this filter, use the following class in the `xmlSitemap.config` of the website and add the whitelist of document types:
```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.DocumentTypeBlacklistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap">
        <documentTypes>
            <add alias="DocumentTypeAlias1" />
            <add alias="DocumentTypeAlias2" />
        </documentTypes>
    </filter>
  </filters>
</umbracoXmlSitemap>
```

### Property Filter
This filter gives the ability remove Umbraco nodes based on their properties and values.

To configure this filter, use the following class in the `xmlSitemap.config` of the website and add the properties to filter on:
```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.PropertiesFilter, MarcelDigital.UmbracoExtensions.XmlSitemap">
        <properties>
            <property alias="propAlias1" value="myValue" operator="equals" required="true" />
        </properties>
    </filter>
  </filters>
</umbracoXmlSitemap>
```

The filter has the following options that can be applied to configure each property to filter on:
* Alias - The alias of the property to filter on.
* Value - The value to check the properties value against.
* Operator - The operator type to check he values against.
  * Values: `equal, unequal`
  * Default: `equal`
* Required - If the node does not have the property, automatically remove.
  * Values: `true, false`
  * Default: `false`
## Chaining Filters
The filters of the sitemap generator can be chained together to create complex filters. This can be done with both the built
in filters and any custom filters that are created. The filter order will be the order that the filters appear in the configuration.

For example, use the following configuration to both remove content that has no template or is of document type 'BadType':
```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.NoTemplateFilter, MarcelDigital.UmbracoExtensions.XmlSitemap" />
    <filter type="MarcelDigital.UmbracoExtensions.XmlSitemap.Filters.DocumentTypeBlacklistFilter, MarcelDigital.UmbracoExtensions.XmlSitemap">
        <documentTypes>
            <add alias="BadAlias" />
        </documentTypes>
    </filter>
  </filters>
</umbracoXmlSitemap>
```
## Custom Content Filters
The sitemap generator can also be used with a custom filter in order to get the Umbraco nodes that you want
the sitemap to display. To do this, implement the `IContentFilter` interface so that the generator will be 
able to filter your content.

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
    public class CustomFilter : IFilter
    {
        public IEnumerable<IPublishedContent> Filter(IEnumerable<IPublishedContent> content) {
            return content.Where(c => c.Level <= 2);
        }
    }
}
```

Then update the `<umbracoXmlSitemap/>` node in the `web.config`. Add the `filter` element with the new class to
the filter list
​    
Example:

```xml
<umbracoXmlSitemap>
  <filters>
    <filter type="MyCool.NameSpace.CustomFilter, MyAssemblyName" />
  </filters>
</umbracoXmlSitemap>
```

## Multi site Umbraco instances

By default the plugin assumes you are running a single site in Umbraco, so it will output all pages starting from the first found root document. If you are running a multi site Umbraco install you can enable a different initializer and cache

```xml
<umbracoXmlSitemap initializer="MarcelDigital.UmbracoExtensions.XmlSitemap.Initializers.DomainInitializer, MarcelDigital.UmbracoExtensions.XmlSitemap"
	cache="MarcelDigital.UmbracoExtensions.XmlSitemap.OptimizationHostnameCache, MarcelDigital.UmbracoExtensions.XmlSitemap">
```

Simply add the attributes initialzer and cache to the `<umbracoXmlSitemap/>` node and populate them with the values above.