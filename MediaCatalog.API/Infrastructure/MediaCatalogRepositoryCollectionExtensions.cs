using MediaCatalog.API.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediaCatalog.API.Infrastructure
{
    public static class MediaCatalogRepositoryCollectionExtensions
    {
        public static IServiceCollection AddMediaCatalogRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            //services.AddScoped<IActorRepository, ActorRepository>();
            //services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            //services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();
            //services.AddScoped<IStudioRepository, StudioRepository>();

            return services;
        }
    }
}