using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IDirectorRepository
    {
        Task<Director[]> GetAllDirectorsAsync();

        Task<Director> GetDirectorAsync(int directorId);

        Task<Director[]> GetDirectorsByMovieIdAsync(int movieId);

        Task<Director> GetDirectorByNameAsync(string lastName, string firstName);
    }
}