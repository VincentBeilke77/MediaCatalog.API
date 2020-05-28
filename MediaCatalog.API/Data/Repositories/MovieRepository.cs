﻿using System.Data;
using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;

namespace MediaCatalog.API.Data.Repositories
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        private readonly MediaCatalogContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(MediaCatalogContext context, ILogger<MovieRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Movie[]> GetAllMoviesAsync()
        {
            _logger.LogInformation($"Getting all Movies");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.MovieGenres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.MovieActors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.MovieDirectors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MovieMediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.MovieStudios)
                .ThenInclude(s => s.Studio);

            query = query.OrderBy(m => m.Title);

            return await query.ToArrayAsync();
        }

        public async Task<Movie> GetMovieAsync(int movieId)
        {
            _logger.LogInformation($"Getting movie info for {movieId}");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.MovieGenres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.MovieActors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.MovieDirectors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MovieMediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.MovieStudios)
                .ThenInclude(s => s.Studio);

            query = query.Where(m => m.Id == movieId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Movie[]> SearchMoviesByTitle(string title)
        {
            _logger.LogInformation($"Getting movies with {title} in them");

            IQueryable<Movie> query = _context.Movies
                .Include(m => m.Rating)
                .Include(gm => gm.MovieGenres)
                .ThenInclude(g => g.Genre)
                .Include(am => am.MovieActors)
                .ThenInclude(a => a.Actor)
                .Include(dm => dm.MovieDirectors)
                .ThenInclude(d => d.Director)
                .Include(mtm => mtm.MovieMediaTypes)
                .ThenInclude(mt => mt.MediaType)
                .Include(sm => sm.MovieStudios)
                .ThenInclude(s => s.Studio);

            query = query.Where(m => m.Title.Contains(title));

            return await query.ToArrayAsync();
        }

        public bool CheckForExistingMovieTitle(string title)
        {
            var exists = _context.Movies.Any(m => m.Title == title);

            return exists;
        }

        public async Task<int> GenerateMovieId()
        {
            _logger.LogInformation("Generating identity for a movie.");

            var id = 0;

            await using var cmd = _context.Database.GetDbConnection().CreateCommand();

            cmd.CommandText = "GetIdentitySeedForTable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter(
                "TableName", "Movie"));

            await _context.Database.OpenConnectionAsync();

            var dr = cmd.ExecuteReaderAsync();

            if (await dr.Result.ReadAsync())
            {
                id = dr.GetAwaiter().GetResult().GetInt32("Id");
            }

            return id;
        }
    }
}