using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie[]> GetAllMoviesAsync();

        Task<Movie> GetMovieAsync(int movieId);

        Task<Movie[]> SearchMoviesByTitle(string title);
    }
}