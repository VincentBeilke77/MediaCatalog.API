using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IMovieRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Movie[]> GetAllMoviesAsync();

        Task<Movie> GetMovieAsync(int movieId);

        Task<Movie[]> SearchMoviesByTitle(string title);

        bool CheckForExistingMovie(string title);

        bool CheckForExistingMovie(int id);

        Task<int> GenerateMovieId();
    }
}