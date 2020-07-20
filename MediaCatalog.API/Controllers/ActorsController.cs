using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using MediaCatalog.API.Data.Entities;

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
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ActorModel[]>> Get()
        {
            try
            {
                var results = await _actorRepository.GetAllActorsAsync();

                return _mapper.Map<ActorModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actorId"></param>
        /// <returns></returns>
        [HttpGet("{actorId}")]
        public async Task<ActionResult<ActorModel>> Get(int actorId)
        {
            try
            {
                var results = await _actorRepository.GetActorAync(actorId);

                return _mapper.Map<ActorModel>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<ActorModel[]>> SearchActorNames(string value)
        {
            try
            {
                var results = await _actorRepository.GetActorsByNameSearchValue(value);

                return _mapper.Map<ActorModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created actor</response>
        /// <response code="400">If the actor model is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ActorModel>> Post(ActorModel model)
        {
            try
            {
                var existing = _actorRepository.GetActorByNameAsync(model.LastName, model.FirstName);

                if (existing != null)
                    return BadRequest(
                        $"The actor, {model.FirstName} {model.LastName}, already exists in the database.");

                var actor = _mapper.Map<Actor>(model);

                actor.Id = await _actorRepository.GenerateActorId();
                var location = _linkGenerator.GetPathByAction("Get", "Actors", new { actorId = actor.Id });

                _actorRepository.Add(actor);

                if (await _actorRepository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<ActorModel>(actor));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        ///
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

                var actor = _actorRepository.GetActorAync(actorId);

                var location = _linkGenerator.GetPathByAction("Get", "Actors", new { actorId = actorId });

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
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
    }
}