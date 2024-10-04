using kim_umbraco.Models;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace kim_umbraco.Business.Services
{
    public class MovieService : IMovieService
    {
        //tillagt för blazor
        private readonly IContentService _contentService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MovieService> _logger;
        //tillagt för blazor
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly string? _apiKey;

        public MovieService(IContentService contentService, HttpClient httpClient, ILogger<MovieService> logger, IConfiguration configuration, IUmbracoContextAccessor umbracoContextAccessor)
        {
            //tillagt för blazor
            _contentService = contentService;
            _httpClient = httpClient;
            _logger = logger;
            //tillagt för blazor
            _umbracoContextAccessor = umbracoContextAccessor;
            _apiKey = configuration["MovieApi:ApiKey"];
        }

        public async Task<List<MovieDetails>> GetMoviesWithDetailsAsync(string query)
        {
            var movies =new List<MovieDetails>();

            try
            {
                var firstRequest = new HttpRequestMessage(HttpMethod.Get, $"https://www.omdbapi.com/?s={query}&apikey={_apiKey}");
                var firstResponse = await _httpClient.SendAsync(firstRequest);

                if (firstResponse.IsSuccessStatusCode)
                {
                    var firstJson = await firstResponse.Content.ReadAsStringAsync();
                    var searchResult = JsonConvert.DeserializeObject<Root>(firstJson);


                    if (searchResult?.Search != null)
                    {
                        foreach ( var movie in searchResult.Search)
                        {
                            var secondRequest = new HttpRequestMessage(HttpMethod.Get, $"https://www.omdbapi.com/?i={movie.ImdbID}&apikey={_apiKey}");
                            var secondResponse = await _httpClient.SendAsync(secondRequest);

                            if (secondResponse.IsSuccessStatusCode)
                            {
                                var secondJson = await secondResponse.Content.ReadAsStringAsync();
                                var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(secondJson);

                                if (movieDetails != null)
                                {
                                    movies.Add(movieDetails);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message); 
            }
            
            return movies;
        }



        //tillagt för blazor
        public bool MovieExists(string id)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content;
                var settingsPage = content.GetAtRoot().DescendantsOrSelf<Settings>().FirstOrDefault();

                if (settingsPage != null)
                {
                    var moviesContainer = settingsPage.MoviesContainer;

                    if (moviesContainer != null)
                    {
                        var movies = moviesContainer.Children<SavedMovie>();

                        foreach (var item in movies)
                        {
                            if (item.ImdbId == id)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void AddMovie(MovieDetails item)
        {
            if (_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                var content = umbracoContext.Content;
                var settingsPage = content?.GetAtRoot().DescendantsOrSelf<Settings>().FirstOrDefault();

                if (settingsPage != null)
                {
                    if (settingsPage.MoviesContainer != null)
                    {
                        var moviePage = _contentService.Create(item.Title, settingsPage.MoviesContainer.Id, nameof(SavedMovie).ToLower());
                        moviePage.SetCultureName(item.Title, item.Culture);
                        moviePage.SetValue("imdbId", item.ImdbID);

                        var save = _contentService.Save(moviePage);

                        if (save.Success)
                        {
                            _contentService.Publish(moviePage, [item.Culture]);
                        }
                    }
                }
            }
        }
    }
}
