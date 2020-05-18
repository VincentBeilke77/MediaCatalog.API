using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public class RatingRepository : BaseRepository, IRatingRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<RatingRepository> _logger;

        public RatingRepository(MediaCatalogContext context, ILogger<RatingRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Rating[]> GetAllRatingsAsyc()
        {
            _logger.LogInformation($"Getting all ratings");

            var query = _context.Ratings;

            return await query.ToArrayAsync();
        }

        public async Task<Rating> GetRatingByIdAsync(int ratingId)
        {
            _logger.LogInformation($"Getting rating by id: {ratingId}");

            var query = _context.Ratings
                .Where(r => r.Id == ratingId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Rating> GetRatingByNameAsync(string name)
        {
            _logger.LogInformation($"Getting rating {name}");

            var query = _context.Ratings
                .Where(r => r.Name == name);

            return await query.FirstOrDefaultAsync();
        }
    }
}