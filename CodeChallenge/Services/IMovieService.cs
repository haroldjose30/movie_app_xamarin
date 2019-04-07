using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Models.Responses;

namespace CodeChallenge.Services
{
    public interface IMovieService
    {
        Task<GenreResponse> GetGenres();
        Task<Movie> GetMovie(int movieId);
        Task<UpcomingMoviesResponse> UpcomingMovies(int page);
    }
}