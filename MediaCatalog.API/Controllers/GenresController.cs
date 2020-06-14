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
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public GenresController(IGenreRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
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

        [HttpGet("{genreId}")]
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

        [HttpPut("{genreId:int}")]
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

        [HttpDelete("{genreId}")]
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