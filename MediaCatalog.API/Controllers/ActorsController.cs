using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace MediaCatalog.API.Controllers
{
    /// <summary>
    /// A Controller used for CRUD Operations on the Actors table in the Media Catalog Database.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        /// Constructor for retrieving the necessary services for the CRUD methods.
        /// </summary>
        /// <param name="actorRepository"></param>
        /// <param name="movieRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        public ActorsController(IActorRepository actorRepository, IMovieRepository movieRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Retrieves all Actors listed in the Media Catalog Database.
        /// </summary>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of all actors,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>ActorModel[]</code>.</response>
        /// <response code="404">If no actors can be found.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet]
        public async Task<ActionResult<ActorModel[]>> Get()
        {
            try
            {
                var results = await _actorRepository.GetAllActorsAsync();

                if (results.Length == 0) return NotFound("No actors found.");

                return Ok(_mapper.Map<ActorModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the Actor linked to the id, and any associated entities.
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with an actor associated with the id,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>ActorModel</code>.</response>
        /// <response code="404">If an actor cannot be found with the requested id.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet("{actorId}")]
        public async Task<ActionResult<ActorModel>> Get(int actorId)
        {
            try
            {
                var results = await _actorRepository.GetActorAsync(actorId);

                if (results == null) return NotFound($"No actor with id, {actorId}, has been found.");

                return Ok(_mapper.Map<ActorModel>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves any Actors with the requested search value, and any associated entities.
        /// </summary>
        /// <param name="value">A <code>String</code>search value for searching all the actor's names.</param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Ok</code> status code with the list of actors that fit the search value,
        /// a <code>NotFound</code> status code if nothing is found,
        /// or a <code>InternalServerError</code> if there is an issue with retrieving the data.
        /// </returns>
        /// <response code="200">Returns an <code>ActionResult</code> containing a <code>ActorModel[]</code>.</response>
        /// <response code="404">If no actors can be found containing the search value.</response>
        /// <response code="500">If there is an issue with the retrieval of the data.</response>
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<ActorModel[]>> SearchActorNames(string value)
        {
            try
            {
                var results = await _actorRepository.GetActorsByNameSearchValue(value);

                if (!results.Any()) return NotFound($"No actors found containing {value} in their name.");

                return Ok(_mapper.Map<ActorModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<ActionResult<ActorModel[]>> GetActorsAssociatedWithMovie(int movieId)
        {
            try
            {
                var results = await _actorRepository.GetActorsByMovieIdAsync(movieId);

                if (!results.Any()) return NotFound("No actors found associated with the movie.");

                return Ok(_mapper.Map<ActorModel[]>(results));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Adds the requested Actor to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// An <code>ActionResult</code> containing:
        /// a <code>Created</code> status code with an ActorModel,
        /// a <code>Conflict</code> status code if there is a conflict with creating the actor,
        /// a <code>NotModified</code> status code if the actor was not created,
        /// a <code>InternalServerError</code> if there is an issue with creating the actor
        /// </returns>
        /// <response code="201">Returns the newly created actor</response>
        /// <response code="409">If there is a conflict with creating the actor</response>
        /// <response code="304">If the actor was not created</response>
        /// <response code="500">If there is an issue with creating the actor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActorModel>> Post(ActorModel model)
        {
            try
            {
                var existing = _actorRepository.GetActorByNameAsync(model.LastName, model.FirstName);

                if (existing != null)
                    return Conflict(
                        $"The actor, {model.FirstName} {model.LastName}, already exists in the database.");

                var actor = _mapper.Map<Actor>(model);

                actor.Id = _actorRepository.GenerateActorId();
                var location = _linkGenerator.GetPathByAction("Get", "Actors", new { actorId = actor.Id });

                _actorRepository.Add(actor);

                if (await _actorRepository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<ActorModel>(actor));
                }

                return Problem($"The actor, {model.FirstName} {model.LastName}, was not created.", "Add Actor",
                    StatusCodes.Status304NotModified);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Connects the associated Movie and Actor in the database.
        /// </summary>
        /// <param name="actorId"></param>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addMovie")]
        public async Task<ActionResult<ActorModel>> Post(int actorId, int movieId)
        {
            try
            {
                // code to retrieve actor, code to verify movie, and return actor with new movie attached
                var movieExists = _movieRepository.CheckForExistingMovie(movieId);

                if (!movieExists) return BadRequest($"The movie id, {movieId}, does not exist.");

                var actor = await _actorRepository.GetActorAsync(actorId);

                var location = _linkGenerator.GetPathByAction("Get", "Actors", new { actorId });

                var actorMovie = new ActorMovie
                {
                    MovieId = movieId,
                    ActorId = actorId
                };

                _actorRepository.Add(actorMovie);

                if (await _actorRepository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<ActorModel>(actor));
                }

                return Problem($"The actor, {actor.FirstName} {actor.LastName}, was added to the movie.",
                    "Post", StatusCodes.Status304NotModified);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}