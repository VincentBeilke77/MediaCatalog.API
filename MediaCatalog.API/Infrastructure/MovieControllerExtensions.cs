using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using MediaCatalog.API.Controllers;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Models;

namespace MediaCatalog.API.Infrastructure
{
    internal static class MovieControllerExtensions
    {
        internal static async Task<ActorMovie> GetMovieActorActionResult(this MoviesController controller, MovieActorsModel movieActor, int movieId)
        {
            if (movieActor.ActorId != 0)
            {
                var actor = await controller.ActorRepository.GetActorAync(movieActor.ActorId);
                return new ActorMovie { ActorId = actor.Id, MovieId = movieId };
            }
            else
            {
                Actor actor = new Actor { LastName = movieActor.LastName, FirstName = movieActor.FirstName };
                controller.ActorRepository.Add(actor);
                if (await controller.ActorRepository.SaveChangesAsync())
                {
                    return new ActorMovie { ActorId = actor.Id, MovieId = movieId };
                }

                return null;
            }
        }

        internal static async Task<DirectorMovie> GetMovieDirectorActionResult(this MoviesController controller, MovieDirectorsModel movieDirector,
            int movieId)
        {
            if (movieDirector.DirectorId != 0)
            {
                var director = await controller.DirectorRepository.GetDirectorAsync(movieDirector.DirectorId);
                return new DirectorMovie { DirectorId = director.Id, MovieId = movieId };
            }
            else
            {
                var director = new Director { LastName = movieDirector.LastName, FirstName = movieDirector.FirstName };
                controller.DirectorRepository.Add(director);
                if (await controller.DirectorRepository.SaveChangesAsync())
                {
                    return new DirectorMovie { DirectorId = director.Id, MovieId = movieId };
                }

                return null;
            }
        }
    }
}