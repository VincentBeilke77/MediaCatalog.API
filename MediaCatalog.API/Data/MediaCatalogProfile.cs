using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.API.Models;

namespace MediaCatalog.API.Data
{
    public class MediaCatalogProfile : Profile
    {
        public MediaCatalogProfile()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(mm => mm.Name, m => m.MapFrom(o => o.Rating.Name))
                .ReverseMap();

            CreateMap<ActorMovie, MovieActorsModel>()
                .ReverseMap();
            CreateMap<DirectorMovie, MovieDirectorsModel>()
                .ReverseMap();
            CreateMap<GenreMovie, MovieGenresModel>()
                .ReverseMap();
            CreateMap<MediaTypeMovie, MovieMediaTypesModel>()
                .ReverseMap();
            CreateMap<StudioMovie, MovieStudiosModel>()
                .ReverseMap();
            CreateMap<Rating, RatingModel>()
                .ReverseMap();
            CreateMap<Genre, GenreModel>()
                .ReverseMap();
            CreateMap<GenreMovie, GenreMoviesModel>()
                .ReverseMap();
        }
    }
}