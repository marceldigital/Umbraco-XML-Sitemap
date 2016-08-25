# XML Sitemap Umbraco Plugin
## Installation
`Install-Package MarcelDigital.UmbracoExtensions.XmlSitemap`
## Override Content Gathering
Currently the sitemap generator will grab all umbraco nodes which have a template. To override this behavior
extend the class `XmlSitemapHandler` and override the method `GetContent()`.

Example:

```csharp
public class CustomSitemapHandler : XmlSitemapHandler {
    protected override IEnumerable<IPublishedContent> GetContent() {
    var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);    
    var content = new List<IPublishedContent>();
    
        // Get your content here

        return content;
    }
}
```