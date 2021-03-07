using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Internal;

namespace MediaCatalog.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        ///
        /// </summary>
        /// <param name="directorRepository"></param>
        /// <param name="movieRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        public DirectorsController(IDirectorRepository directorRepository, IMovieRepository movieRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            _directorRepository = directorRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Retrieves all Directors found in the Media Catalog Database.
        /// </summary>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of all directors,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>DirectorModel[]</code>.</response>
        /// <response code="404">If no directors can be found.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DirectorModel[]>> Get()
        {
            try
            {
                var results = await _directorRepository.GetAllDirectorsAsync();

                if (results.Length == 0) return NotFound("No Directors found.");

                return Ok(_mapper.Map<DirectorModel>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the director linked to the id.
        /// </summary>
        /// <param name="directorId">Id of the director being looked for.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the director related to the id,
        /// a <code>NotFound</code> status code if no director was found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>DirectorModel</code>.</response>
        /// <response code="404">If a director cannot be found with the requested id.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet("{directorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DirectorModel>> Get(int directorId)
        {
            try
            {
                var result = await _directorRepository.GetDirectorAsync(directorId);

                if (result == null) return NotFound($"No director with id, {directorId}, was found.");

                return Ok(_mapper.Map<DirectorModel>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves any Directors with the requested search value, and any associated entities.
        /// </summary>
        /// <param name="search">A <code>String</code>search value for searching all the director's names.</param>>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of directors that fit the search value,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>DirectorModel[]</code>.</response>
        /// <response code="404">If no directors can be found containing the search value.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet("search/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DirectorModel[]>> SearchByDirectorName(string search)
        {
            try
            {
                var results = await _directorRepository.SearchByDirectorsName(search);

                if (!results.Any()) return NotFound($"No directors with {search} in them where found.");

                return Ok(_mapper.Map<DirectorModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// ToDo Create method body to retrieve directors associated with a specific movie, should return a 200, 404, or 500 error depending on what happens
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("search/{movieId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DirectorModel[]>> GetDirectorsAssociatedWithMovie(int movieId)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// ToDo Create method body to add a director to the database, should return a 201 or 500 error depending on what happens
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DirectorModel>> Post(DirectorModel model)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            throw new NotImplementedException();
        }
    }
}