using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MediaCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;

        public GenresController(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}