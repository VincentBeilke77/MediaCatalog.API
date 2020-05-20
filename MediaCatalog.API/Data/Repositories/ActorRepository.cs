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
    public class ActorRepository : BaseRepository, IActorRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<BaseRepository> _logger;

        public ActorRepository(MediaCatalogContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Actor> GetActorAync(int actorId)
        {
            _logger.LogInformation($"Getting actor info for {actorId}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(a => a.Id == actorId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Actor> GetActorByNameAsync(string lastName, string firstName)
        {
            _logger.LogInformation($"Getting actor info for {lastName}, {firstName}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query
                .Where(a => a.LastName == lastName && a.FirstName == firstName);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Actor[]> GetActorsByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting actor info for {movieId}");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.Where(a => a.Movies.Any(am => am.MovieId == movieId));

            return await query.ToArrayAsync();
        }

        public async Task<Actor[]> GetAllActorsAsync()
        {
            _logger.LogInformation($"Getting all actors info");

            IQueryable<Actor> query = _context.Actors
                .Include(am => am.Movies)
                .ThenInclude(m => m.Movie);

            query = query.OrderBy(a => a.FullName);

            return await query.ToArrayAsync();
        }
    }
}