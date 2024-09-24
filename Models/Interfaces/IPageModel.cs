using Umbraco.Cms.Core.Models.PublishedContent;

namespace kim_umbraco.Models.Interfaces
{
	public interface IPageModel : IPublishedContent
	{
		IPublishedContent Content { get; }
	}
}
