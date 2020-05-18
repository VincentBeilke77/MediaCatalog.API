using MediaCatalog.API.Data.Entities;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public interface IRatingRepository
    {
        Task<Rating[]> GetAllRatingsAsyc();

        Task<Rating> GetRatingByIdAsync(int ratingId);

        Task<Rating> GetRatingByNameAsync(string name);
    }
}