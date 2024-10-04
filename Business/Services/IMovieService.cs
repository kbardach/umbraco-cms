using kim_umbraco.Models;

namespace kim_umbraco.Business.Services
{
    public interface IMovieService
    {
        Task<List<MovieDetails>> GetMoviesWithDetailsAsync(string query);


        //tillagt för blazor
        bool MovieExists(string id);

        void AddMovie(MovieDetails item);
    }
}
