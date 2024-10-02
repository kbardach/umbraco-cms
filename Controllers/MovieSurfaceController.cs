using kim_umbraco.Models;
using kim_umbraco.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;

namespace kim_umbraco.Controllers
{
    public class MovieSurfaceController : SurfaceController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public MovieSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MovieDetails item)
        {
            var page = CurrentPage as Search;

            var model = new MoviePageViewModel(page, _umbracoContextAccessor);
            model.Movie = item;

            return View("movie", model);
        }
    }
}


//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Index(MovieDetails movie)
//{

//    //todo skapa viewmodel
//    return View("movie");
//}
