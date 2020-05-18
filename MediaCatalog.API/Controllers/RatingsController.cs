using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly RatingRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public RatingsController(IRatingRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = (RatingRepository)repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

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
            catch (SqlException sqlEx)
            {
                return StatusCode(StatusCodes.Status409Conflict, sqlEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest();
        }
    }
}