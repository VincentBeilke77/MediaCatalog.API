using AutoMapper;
using MediaCatalog.API.Data.Entities;
using MediaCatalog.Models;

namespace MediaCatalog.API.Data
{
    public class MediaCatalogProfile : Profile
    {
        public MediaCatalogProfile()
        {
            CreateMap<Movie, MovieModel>()
                .ReverseMap()
                .ForMember(mm => mm.Rating, opt => opt.Ignore());

            CreateMap<Rating, MovieRatingModel>()
                .ReverseMap();
            CreateMap<ActorMovie, MovieActorsModel>()
                .ForMember(mam => mam.FirstName, am => am.MapFrom(a => a.Actor.FirstName))
                .ForMember(mam => mam.LastName, am => am.MapFrom(a => a.Actor.LastName))
                .ReverseMap();
            CreateMap<DirectorMovie, MovieDirectorsModel>()
                .ForMember(mdm => mdm.FirstName, dm => dm.MapFrom(d => d.Director.FirstName))
                .ForMember(mdm => mdm.LastName, dm => dm.MapFrom(d => d.Director.LastName))
                .ReverseMap();
            CreateMap<GenreMovie, MovieGenresModel>()
                .ForMember(mgm => mgm.Name, gm => gm.MapFrom(g => g.Genre.Name))
                .ForMember(mgm => mgm.Description, gm => gm.MapFrom(g => g.Genre.Description))
                .ReverseMap();
            CreateMap<MediaTypeMovie, MovieMediaTypesModel>()
                .ForMember(mmtm => mmtm.Name, mtm => mtm.MapFrom(mt => mt.MediaType.Name))
                .ForMember(mmtm => mmtm.Description, mtm => mtm.MapFrom(mt => mt.MediaType.Description))
                .ReverseMap();
            CreateMap<StudioMovie, MovieStudiosModel>()
                .ForMember(msm => msm.Name, sm => sm.MapFrom(s => s.Studio.Name))
                .ForMember(msm => msm.Description, sm => sm.MapFrom(s => s.Studio.Description))
                .ReverseMap();

            CreateMap<Rating, RatingModel>()
                .ReverseMap();

            CreateMap<Genre, GenreModel>()
                .ReverseMap();
            CreateMap<GenreMovie, GenreMoviesModel>()
                .ReverseMap();

            CreateMap<Actor, ActorModel>()
                .ReverseMap();
            CreateMap<ActorMovie, ActorMoviesModel>()
                .ReverseMap();
        }
    }
}