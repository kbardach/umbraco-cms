using Umbraco.Cms.Core.Models.PublishedContent;

namespace kim_umbraco.Business.Services
{
	public interface ISitemapService
	{
		List<IPublishedContent> Pages();
	}
}
