using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;
using Microsoft.AspNetCore.Routing;

namespace MediaCatalog.API.Controllers
{
    /// <summary>
    /// A controller for CRUD operations on the Genres Table in the Media Catalog Database.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Constructor for retrieving the necessary services for the CRUD methods.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        public GenresController(IGenreRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Retrieves all the Genres listed in the Media Catalog database, along with all the movies
        /// associated with each Genre.
        /// </summary>
        /// <returns>A <code>IEnumerable</code> of <code>GenreModel</code></returns>
        /// <response code="200">Returns a <code>IEnumerable</code> of <code>GenreModel</code></response>
        /// <response code="500">If there is an issue with the retrieval of the data</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllGenresAsync();

                return _mapper.Map<GenreModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the Genre linked to the id, along with all the movies
        /// associated with Genre.
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns>A <code>GenreModel</code></returns>
        /// <response code="200">Returns a <code>GenreModel</code></response>
        /// <response code="500">If there is an issue with the retrieval of the data</response>
        [HttpGet("{genreId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreModel>> Get(int genreId)
        {
            try
            {
                var result = await _repository.GetGenreAsync(genreId);

                if (result == null) return NotFound();

                return _mapper.Map<GenreModel>(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new Genre, and returns the newly created Genre.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created genre</response>
        /// <response code="400">If the genre model already exists in the database</response>
        /// <response code="500">If there is an issue with the retrieval of the data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreModel>> Post(GenreModel model)
        {
            try
            {
                var existing = _repository.CheckForExistingGenreName(model.Name);

                if (existing) return BadRequest($"The genre, {model.Name}, already exists in database.");

                var genre = _mapper.Map<Genre>(model);

                var genreId = _repository.GenerateGenreId();

                var location = _linkGenerator.GetPathByAction("Get", "Genres", new { genreId = genreId.Id });

                _repository.Add(genre);

                if (await _repository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<GenreModel>(genre));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates the Genre being passed in, and returns the updated Genre.
        /// </summary>
        /// <param name="genreId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="400">If the genre to be updated does not exists in the database</response>
        /// <response code="500">If there is an issue with the retrieval of the data</response>
        [HttpPut("{genreId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreModel>> Put(int genreId, GenreModel model)
        {
            try
            {
                var oldGenre = await _repository.GetGenreAsync(genreId);

                if (oldGenre == null) return BadRequest($"The genre, {model.Name}, does not exist in the database.");

                _mapper.Map(model, oldGenre);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<GenreModel>(oldGenre);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes the requested Genre from the database.
        /// </summary>
        /// <param name="genreId"></param>
        /// <returns></returns>
        /// <response code="200">returns an ok if the genre is successfully deleted</response>
        /// <response code="400">If the genre to be updated does not exists in the database</response>
        /// <response code="500">If there is an issue with the retrieval of the data</response>
        [HttpDelete("{genreId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int genreId)
        {
            try
            {
                var oldGenre = await _repository.GetGenreAsync(genreId);

                if (oldGenre == null) return NotFound($"Could not find genre with id of {genreId}");

                _repository.Delete(oldGenre);

                if (await _repository.SaveChangesAsync())
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