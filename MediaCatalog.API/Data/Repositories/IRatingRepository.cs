using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IRatingRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        Task<Rating[]> GetAllRatingsAsyc();

        Task<Rating> GetRatingByIdAsync(int ratingId);

        Task<Rating> GetRatingByNameAsync(string name);
    }
}