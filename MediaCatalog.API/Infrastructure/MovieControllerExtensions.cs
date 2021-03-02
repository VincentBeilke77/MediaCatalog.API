using System.Threading.Tasks;
using MediaCatalog.API.Controllers;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.Models;

namespace MediaCatalog.API.Infrastructure
{
    internal static class MovieControllerExtensions
    {
        internal static async Task<ActorMovie> GetMovieActorActionResult(this MoviesController controller, MovieActorsModel movieActor, int movieId)
        {
            if (movieActor.ActorId != 0)
            {
                var actor = await controller.ActorRepository.GetActorAsync(movieActor.ActorId);
                return new ActorMovie { ActorId = actor.Id, MovieId = movieId };
            }
            else
            {
                var actorId = controller.ActorRepository.GenerateActorId();

                Actor actor = new Actor { Id = actorId, LastName = movieActor.LastName, FirstName = movieActor.FirstName };
                controller.ActorRepository.Add(actor);
                if (await controller.ActorRepository.SaveChangesAsync())
                {
                    return new ActorMovie { ActorId = actorId, MovieId = movieId };
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
                var directorId = await controller.DirectorRepository.GenerateDirectorId();

                var director = new Director { Id = directorId, LastName = movieDirector.LastName, FirstName = movieDirector.FirstName };
                controller.DirectorRepository.Add(director);
                if (await controller.DirectorRepository.SaveChangesAsync())
                {
                    return new DirectorMovie { DirectorId = directorId, MovieId = movieId };
                }

                return null;
            }
        }

        internal static async Task<StudioMovie> GetMovieStudioActionResult(this MoviesController controller, MovieStudiosModel movieStudios,
            int movieId)
        {
            if (movieStudios.StudioId != 0)
            {
                var director = await controller.StudioRepository.GetStudioAsync(movieStudios.StudioId);
                return new StudioMovie { StudioId = director.Id, MovieId = movieId };
            }
            else
            {
                var studioId = await controller.DirectorRepository.GenerateDirectorId();

                var studio = new Studio { Id = studioId, Name = movieStudios.Name, Description = movieStudios.Description };
                controller.DirectorRepository.Add(studio);
                if (await controller.DirectorRepository.SaveChangesAsync())
                {
                    return new StudioMovie { StudioId = studioId, MovieId = movieId };
                }

                return null;
            }
        }
    }
}