using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediaCatalog.API.Data.Repositories
{
    public class DirectorRepository : BaseRepository, IDirectorRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<BaseRepository> _logger;

        public DirectorRepository(MediaCatalogContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Director[]> GetAllDirectorsAsync()
        {
            _logger.LogInformation($"Getting all directors.");

            IQueryable<Director> query = _context.Directors
                .Include(dm => dm.DirectorMovies)
                .ThenInclude(m => m.Movie);

            query = query.OrderBy(d => d.FullName);

            return await query.ToArrayAsync();
        }

        public async Task<Director> GetDirectorAsync(int directorId)
        {
            _logger.LogInformation($"Getting director info for {directorId}");

            IQueryable<Director> query = _context.Directors
                .Include(dm => dm.DirectorMovies)
                .ThenInclude(m => m.Movie);

            query = query.Where(d => d.Id == directorId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Director[]> GetDirectorsByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting director info for {movieId}");

            IQueryable<Director> query = _context.Directors
                .Include(dm => dm.DirectorMovies)
                .ThenInclude(m => m.Movie);

            query = query.Where(dm => dm.DirectorMovies.Any(m => m.MovieId == movieId));

            return await query.ToArrayAsync();
        }

        public async Task<Director> GetDirectorByNameAsync(string lastName, string firstName)
        {
            _logger.LogInformation($"Getting director info for {lastName},{firstName}");

            IQueryable<Director> query = _context.Directors
                .Include(dm => dm.DirectorMovies)
                .ThenInclude(m => m.Movie);

            query = query.Where(d => d.LastName == lastName && d.FirstName == firstName);

            return await query.FirstOrDefaultAsync();
        }
    }
}