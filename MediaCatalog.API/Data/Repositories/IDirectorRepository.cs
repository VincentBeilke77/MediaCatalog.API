using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IDirectorRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Director[]> GetAllDirectorsAsync();

        Task<Director> GetDirectorAsync(int directorId);

        Task<Director[]> GetDirectorsByMovieIdAsync(int movieId);

        Task<Director> GetDirectorByNameAsync(string lastName, string firstName);

        Task<int> GenerateDirectorId();
    }
}