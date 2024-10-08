using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace kim_umbraco.Models.ViewModels
{
    public class SitemapViewModel : BasePageModel<Sitemap>
    {
        public List<IPublishedContent> Pages { get; set; }

        public SitemapViewModel(Sitemap content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}
