using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace kim_umbraco.Models.ViewModels
{
    public class SearchPageViewModel : BasePageModel<Search>
    {
        public SearchPageViewModel(Search content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }

        public List<MovieDetails> Movies { get; set; } = [];
    }
}
