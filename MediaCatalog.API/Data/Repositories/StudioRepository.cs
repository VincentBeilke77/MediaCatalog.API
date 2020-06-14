using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MediaCatalog.API.Data.Repositories
{
    public class StudioRepository : BaseRepository, IStudioRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<BaseRepository> _logger;

        public StudioRepository(MediaCatalogContext context, ILogger<BaseRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GenerateStudioId()
        {
            _logger.LogInformation("Generating identity for a studio.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Studio"));

            await _context.Database.OpenConnectionAsync();

            var dr = cmd.ExecuteReaderAsync();

            if (await dr.Result.ReadAsync())
            {
                id = dr.GetAwaiter().GetResult().GetInt32("Id");
            }

            return id;
        }

        public async Task<Studio[]> GetAllStudiosAsync()
        {
            _logger.LogInformation($"Getting all Studios");
            IQueryable<Studio> query = _context.Studios
                .Include(sm => sm.StudioMovies)
                .ThenInclude(m => m.Movie);

            query = query.OrderBy(s => s.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Studio> GetStudioAsync(int studioId)
        {
            _logger.LogInformation($"Getting studio info for {studioId}");

            IQueryable<Studio> query = _context.Studios
                .Include(sm => sm.StudioMovies)
                .ThenInclude(m => m.Movie);

            query = query.Where(s => s.Id == studioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Studio[]> GetStudiosByMovieIdAsync(int movieId)
        {
            _logger.LogInformation($"Getting Studios for {movieId}");

            IQueryable<Studio> query = _context.Studios
                .Include(sm => sm.StudioMovies)
                .ThenInclude(m => m.Movie);

            query = query.Where(s => s.StudioMovies.Any(sm => sm.MovieId == movieId))
                .OrderBy(s => s.Name);

            return await query.ToArrayAsync();
        }
    }
}