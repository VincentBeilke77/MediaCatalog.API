using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.API.Infrastructure;
using MediaCatalog.API.Migrations;

namespace MediaCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        internal IActorRepository ActorRepository { get; }
        internal IDirectorRepository DirectorRepository { get; }
        public IStudioRepository StudioRepository { get; }

        private readonly IMovieRepository _movieRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public MoviesController(IMovieRepository movieRepository,
            IRatingRepository ratingRepository, IGenreRepository genreRepository,
            IActorRepository actorRepository, IDirectorRepository directorRepository,
            IStudioRepository studioRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            ActorRepository = actorRepository;
            DirectorRepository = directorRepository;
            StudioRepository = studioRepository;
            _movieRepository = movieRepository;
            _ratingRepository = ratingRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<MovieModel[]>> Get()
        {
            try
            {
                var results = await _movieRepository.GetAllMoviesAsync();

                return _mapper.Map<MovieModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieModel>> Get(int movieId)
        {
            try
            {
                var result = await _movieRepository.GetMovieAsync(movieId);

                if (result == null) return NotFound();

                return _mapper.Map<MovieModel>(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<MovieModel[]>> SearchByTitle(string title)
        {
            try
            {
                var results = await _movieRepository.SearchMoviesByTitle(title);

                if (!results.Any()) return NotFound();

                return _mapper.Map<MovieModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        public async Task<ActionResult<MovieModel>> Post(MovieModel model)
        {
            try
            {
                var existing = _movieRepository.CheckForExistingMovieTitle(model.Title);

                if (existing) return BadRequest($"The movie title, {model.Title}, already exists in database.");

                var movie = _mapper.Map<Movie>(model);

                movie.Id = await _movieRepository.GenerateMovieId();
                var location = _linkGenerator.GetPathByAction("Get", "Movies", new { movieId = movie.Id });

                if (string.IsNullOrWhiteSpace(location)) return BadRequest("Could not generate a movie id.");

                if (model.Rating != null)
                {
                    var rating = await _ratingRepository.GetRatingByIdAsync(model.Rating.Id);
                    if (rating == null) return BadRequest($"The rating id, {model.Rating.Id}, does not exist.");
                    movie.Rating = rating;
                }

                if (model.MovieGenres != null)
                {
                    var movieGenres = new List<GenreMovie>();
                    foreach (var movieGenre in model.MovieGenres)
                    {
                        var genre = await _genreRepository.GetGenreAsync(movieGenre.GenreId);
                        if (genre == null) return BadRequest($"The genre id, {movieGenre.GenreId}, does not exist.");
                        var mg = new GenreMovie { GenreId = genre.Id, MovieId = model.Id };
                        movieGenres.Add(mg);
                    }

                    movie.MovieGenres = movieGenres;
                }

                if (model.MovieActors != null)
                {
                    var movieActors = new List<ActorMovie>();
                    foreach (var movieActor in model.MovieActors)
                    {
                        var actorMovie = await this.GetMovieActorActionResult(movieActor, model.Id);
                        if (actorMovie == null) return BadRequest($"Could not add actor {movieActor.LastName}, {movieActor.FirstName}");
                        movieActors.Add(actorMovie);
                    }

                    movie.MovieActors = movieActors;
                }

                if (model.MovieDirectors != null)
                {
                    var movieDirectors = new List<DirectorMovie>();
                    foreach (var movieDirector in model.MovieDirectors)
                    {
                        var directorMovie = await this.GetMovieDirectorActionResult(movieDirector, model.Id);
                        if (directorMovie == null) return BadRequest($"Could not add director {movieDirector.LastName}, {movieDirector.FirstName}");
                        movieDirectors.Add(directorMovie);
                    }

                    movie.MovieDirectors = movieDirectors;
                }

                if (model.MovieStudios != null)
                {
                    var movieStudios = new List<StudioMovie>();
                    foreach (var movieStudio in model.MovieStudios)
                    {
                        var studioMovie = await this.GetMovieStudioActionResult(movieStudio, model.Id);
                        if (studioMovie == null) return BadRequest($"Could not add the studio, {movieStudio.Name}");
                        movieStudios.Add(studioMovie);
                    }

                    movie.MovieStudios = movieStudios;
                }

                _movieRepository.Add(movie);

                if (await _movieRepository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<MovieModel>(movie));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpPut("{movieId:int}")]
        public async Task<ActionResult<MovieModel>> Put(int movieId, MovieModel model)
        {
            try
            {
                var oldMovie = await _movieRepository.GetMovieAsync(movieId);

                if (oldMovie == null) return BadRequest($"The movie title, {model.Title}, does not exist in the database.");

                _mapper.Map(model, oldMovie);

                if (await _movieRepository.SaveChangesAsync())
                {
                    return _mapper.Map<MovieModel>(oldMovie);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> Delete(int movieId)
        {
            try
            {
                var oldMovie = await _movieRepository.GetMovieAsync(movieId);

                if (oldMovie == null) return NotFound($"Could not find movie with id of {movieId}");

                _movieRepository.Delete(oldMovie);

                if (await _movieRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
    }
}