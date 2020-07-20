using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace MediaCatalog.API.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly RatingRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="linkGenerator"></param>
        public RatingsController(IRatingRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = (RatingRepository)repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<RatingModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllRatingsAsyc();

                return _mapper.Map<RatingModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        [HttpGet("{ratingId}")]
        public async Task<ActionResult<RatingModel>> Get(int ratingId)
        {
            try
            {
                var results = await _repository.GetRatingByIdAsync(ratingId);

                return _mapper.Map<RatingModel>(results);
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
        [HttpPost]
        public async Task<ActionResult<RatingModel>> Post(RatingModel model)
        {
            try
            {
                var existing = await _repository.GetRatingByNameAsync(model.Name);

                if (existing != null)
                {
                    return BadRequest($"The rating for {model.Name} already exists!");
                }

                var rating = _mapper.Map<Rating>(model);
                _repository.Add(rating);
                if (await _repository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Ratings", new { ratingId = rating.Id });

                    return Created(location, _mapper.Map<RatingModel>(rating));
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
        /// <param name="ratingId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{ratingId}")]
        public async Task<ActionResult<RatingModel>> Put(int ratingId, RatingModel model)
        {
            try
            {
                var oldRating = await _repository.GetRatingByIdAsync(ratingId);
                if (oldRating == null) return NotFound($"Could not find rating with with id of {ratingId}");

                _mapper.Map(model, oldRating);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<RatingModel>(oldRating);
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
        /// <param name="ratingId"></param>
        /// <returns></returns>
        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> Delete(int ratingId)
        {
            try
            {
                var oldRating = await _repository.GetRatingByIdAsync(ratingId);
                if (oldRating == null) return NotFound($"Could not find rating with with id of {ratingId}");

                _repository.Delete(oldRating);

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