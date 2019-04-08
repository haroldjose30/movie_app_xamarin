using System.Collections.Generic;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;

namespace CodeChallenge.Services
{
    public interface IMovieService
    {
        Task<List<Genre>> GetGenres();
        List<Genre> GetGenresCached();
        Task<Movie> GetMovie(int movieId);
        Task<SearchMovieResponse> SearchMovie(string query, int page);
        Task<UpcomingMoviesResponse> UpcomingMovies(int page);
    }
}