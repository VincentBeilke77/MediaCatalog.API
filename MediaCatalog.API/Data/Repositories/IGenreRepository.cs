using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre[]> GetAllGenresAsync();

        Task<Genre> GetGenreAsync(int genreId);
    }
}