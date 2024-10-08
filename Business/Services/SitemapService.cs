using kim_umbraco.Business.Services;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace kim_umbraco.Business.Services
{
	public class SitemapService : ISitemapService
	{
		private readonly IUmbracoContextAccessor _umbracoContextAccessor;

		public SitemapService(IUmbracoContextAccessor umbracoContextAccessor)
		{
			_umbracoContextAccessor = umbracoContextAccessor;
		}

		public List<IPublishedContent> Pages()
		{
			if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
			{
				var content = umbracoContext.Content;

				if (content != null)
				{
					var startPage = content.GetAtRoot().DescendantsOrSelf<Start>().FirstOrDefault();

					if (startPage != null)
					{
						return startPage.DescendantsOrSelf<IPublishedContent>()
							.Where(page => page is ISeo seoPage).ToList();
					}
				}
			}

			return [];
		}
	}
}




//public class SitemapService : ISitemapService
//{
//    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

//    // Konstruktor som tar IUmbracoContextAccessor via Dependency Injection
//    public SitemapService(IUmbracoContextAccessor umbracoContextAccessor)
//    {
//        _umbracoContextAccessor = umbracoContextAccessor;
//    }

//    // Hämtar alla sidor (Pages) som ska inkluderas i sitemapen
//    public List<IPublishedContent> Pages()
//    {
//        // Försök att få tillgång till UmbracoContext genom IUmbracoContextAccessor
//        if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
//        {
//            // Hämtar Umbraco-content (träd med innehållssidor)
//            var content = umbracoContext.Content;

//            if (content != null)
//            {
//                // Hämtar start-sidan (roten) av webbplatsen
//                var startPage = content.GetAtRoot().DescendantsOrSelf<Start>().FirstOrDefault();

//                if (startPage != null)
//                {
//                    // Hämtar alla sidor under start-sidan (inklusive sig själv) som implementerar ISeo och returnerar dem som en lista
//                    return startPage.DescendantsOrSelf<IPublishedContent>()
//                        .Where(page => page is ISeo seoPage).ToList();
//                }
//            }
//        }

//        // Returnerar en tom lista om UmbracoContext eller start-sidan inte hittas
//        return new List<IPublishedContent>();
//    }
//}
