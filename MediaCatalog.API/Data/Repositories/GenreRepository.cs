using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Repositories
{
    public class GenreRepository : BaseRepository, IGenreRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<GenreRepository> _logger;

        public GenreRepository(MediaCatalogContext context, ILogger<GenreRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Genre[]> GetAllGenresAsync()
        {
            _logger.LogInformation($"Getting all Genres");

            IQueryable<Genre> query = _context.Genres;

            query = query.OrderBy(g => g.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Genre> GetGenreAsync(int genreId)
        {
            _logger.LogInformation($"Getting Genre for id: {genreId}");

            IQueryable<Genre> query = _context.Genres
                .Include(gm => gm.GenreMovies)
                .ThenInclude(m => m.Movie);

            query = query
                .Where(g => g.Id == genreId);

            return await query.FirstOrDefaultAsync();
        }
    }
}