using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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

        public async Task<int> GenerateGenreId()
        {
            _logger.LogInformation("Generating identity for a genre.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Genre"));

            await _context.Database.OpenConnectionAsync();

            var dr = cmd.ExecuteReaderAsync();

            if (await dr.Result.ReadAsync())
            {
                id = dr.GetAwaiter().GetResult().GetInt32("Id");
            }

            return id;
        }

        public bool CheckForExistingGenreName(string name)
        {
            var exists = _context.Genres.Any(g => g.Name.ToLower() == name.ToLower());

            return exists;
        }
    }
}