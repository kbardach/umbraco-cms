using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace kim_umbraco.Business.Extensions
{
    public static class ContentExtensions
    {
        public static string GetFullUrl(this IPublishedContent content, string language)
        {
            // Resolve IUmbracoContextAccessor from the current service provider
            var umbracoContextAccessor = StaticServiceProvider.Instance.GetService<IUmbracoContextAccessor>();

            if (umbracoContextAccessor?.TryGetUmbracoContext(out var umbracoContext) ?? false)
            {
                // Ensure the culture exists for the content
                if (content.Cultures.ContainsKey(language))
                {
                    // Generate the full URL for the specified language variant
                    return content.Url(mode: UrlMode.Absolute, culture: language);
                }
            }

            return string.Empty;
        }
    }
}


//public static class ContentExtensions
//{
//    // Extension method to get the full URL of a content item for a specific language variant
//    public static string GetFullUrl(this IPublishedContent content, string language)
//    {
//        // Resolve IUmbracoContextAccessor from the current service provider (to get access to the Umbraco context)
//        var umbracoContextAccessor = StaticServiceProvider.Instance.GetService<IUmbracoContextAccessor>();

//        // Check if Umbraco context is available and successfully resolved
//        if (umbracoContextAccessor?.TryGetUmbracoContext(out var umbracoContext) ?? false)
//        {
//            // Ensure the content has a culture variant for the specified language
//            if (content.Cultures.ContainsKey(language))
//            {
//                // Generate and return the full (absolute) URL for the specified language variant
//                return content.Url(mode: UrlMode.Absolute, culture: language);
//            }
//        }

//        // Return an empty string if the language or Umbraco context is unavailable
//        return string.Empty;
//    }
//}
