using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
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
    }
}