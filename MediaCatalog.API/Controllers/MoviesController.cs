using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCatalog.API.Infrastructure;

namespace MediaCatalog.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
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

        /// <summary>
        /// Constructor that sets up the repositories for the controller.
        /// </summary>
        /// <param name="movieRepository">Interface for the movies.</param>
        /// <param name="ratingRepository">Interface for the ratings.</param>
        /// <param name="genreRepository">Interface for the genres.</param>
        /// <param name="actorRepository">Interface for the actors.</param>
        /// <param name="directorRepository">Interface for the directors.</param>
        /// <param name="studioRepository">Interface for the studios.</param>
        /// <param name="mapper">Interface for mapping models to entities and vice versa.</param>
        /// <param name="linkGenerator">Used to generate a url path based on the action being used.</param>
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

        /// <summary>
        /// Retrieves all the Movies in the Media Catalog database, and all associated entities.
        /// </summary>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of movies,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>MovieModel[]</code>.</response>
        /// <response code="404">If no movies can be found.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet]
        public async Task<ActionResult<MovieModel[]>> Get()
        {
            try
            {
                var results = await _movieRepository.GetAllMoviesAsync();

                if (results.Length == 0) return NotFound("No movies found.");

                return Ok(_mapper.Map<MovieModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the Movie linked to the id, along with all associated entities.
        /// </summary>
        /// <param name="movieId">Id of the movie being looked for.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the movie related to the id,
        /// a <code>NotFound</code> status code if no movie was found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>MovieModel</code>.</response>
        /// <response code="404">If a movie cannot be found with the requested id.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieModel>> Get(int movieId)
        {
            try
            {
                var result = await _movieRepository.GetMovieAsync(movieId);

                if (result == null) return NotFound($"No movie with id, {movieId}, was found.");

                return Ok(_mapper.Map<MovieModel>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves any Movies with the requested search value, and any associated entities.
        /// </summary>
        /// <param name="search">A <code>String</code>search value for searching all the movie titles.</param>>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of movies that fit the search value,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>MovieModel[]</code>.</response>
        /// <response code="404">If no movies can be found containing the search value.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet("search")]
        public async Task<ActionResult<MovieModel[]>> SearchByTitle(string search)
        {
            try
            {
                var results = await _movieRepository.SearchMoviesByTitle(search);

                if (!results.Any()) return NotFound($"No movies with {search} in them where found.");

                return Ok(_mapper.Map<MovieModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new Movie, and returns the newly created Movie.
        /// </summary>
        /// <param name="model">The <code>MovieModel</code> containing the new movie to be updated.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Create</code> status code with the newly created movie,
        /// a <code>BadRequest</code> status code if their is an existing movie with the title given already,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="201">Returns the newly created <Code>MovieModel</Code>.</response>
        /// <response code="400">If there is existing movie with the title given. Also if there is an issue with
        /// creating the new movie, such as a non-existing rating, genre,  actor, director or studio.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpPost]
        public async Task<ActionResult<MovieModel>> Post(MovieModel model)
        {
            try
            {
                var existing = _movieRepository.CheckForExistingMovie(model.Title);

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

        /// <summary>
        /// Updates the requested Movie, and returns the updated Movie.
        /// </summary>
        /// <param name="movieId">An <code>Integer</code> containing the id of the movie to  be updated.</param>
        /// <param name="model">A <code>MovieModel</code> containing the movie with the updated values to be saved.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the now updated movie,
        /// a <code>BadRequest</code> status code if their is not an existing movie with the title given,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns the updated <Code>MovieModel.</Code></response>
        /// <response code="400">If the Movie to be updated does not exists in the database.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpPut("{genreId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    return Ok(_mapper.Map<MovieModel>(oldMovie));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest($"The movie, {model.Title}, was not updated.");
        }

        /// <summary>
        /// Deletes the requested Movie.
        /// </summary>
        /// <param name="movieId">An <code>Integer</code> containing the id of the movie to be deleted.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code and the movie has been deleted,
        /// a <code>BadRequest</code> status code if their is not an existing movie with the title given,
        /// or a <code>InternalServerError</code> if there is an issue with deleting the data.
        /// </returns>
        /// <response code="200">returns an ok if the Moive is successfully deleted.</response>
        /// <response code="400">If the Movie to be deleted does not exists in the database</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
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
                    return Ok($"The movie for movie id, {movieId}, was deleted.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest($"The movie with the id, {movieId}, was not deleted.");
        }
    }
}