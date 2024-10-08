using kim_umbraco.Business.Services;
using kim_umbraco.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace kim_umbraco.Controllers
{
    public class SitemapController : RenderController
    {
        private readonly ISitemapService _sitemapService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SitemapController(ISitemapService sitemapService, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _sitemapService = sitemapService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            var sitemap = CurrentPage as Sitemap;

            if (sitemap != null)
            {
                var model = new SitemapViewModel(sitemap, _umbracoContextAccessor)
                {
                    Pages = _sitemapService.Pages()
                };

                return View("sitemap", model);
            }

            return null;
        }
    }
}
