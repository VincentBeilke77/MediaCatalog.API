using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using MediaCatalog.API.Data.Repositories;
using MediaCatalog.API.Models;
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
    }
}