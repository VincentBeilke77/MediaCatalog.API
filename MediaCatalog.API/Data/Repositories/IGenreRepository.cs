using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IGenreRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Genre[]> GetAllGenresAsync();

        Task<Genre> GetGenreAsync(int genreId);

        Task<int> GenerateGenreId();

        bool CheckForExistingGenreName(string name);
    }
}