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
                .ReverseMap();

            CreateMap<ActorMovie, MovieActorsModel>()
                .ForMember(mam => mam.Id, am => am.MapFrom(a => a.Actor.Id))
                .ForMember(mam => mam.FirstName, am => am.MapFrom(a => a.Actor.FirstName))
                .ForMember(mam => mam.LastName, am => am.MapFrom(a => a.Actor.LastName))
                .ReverseMap();
            CreateMap<DirectorMovie, MovieDirectorsModel>()
                .ForMember(mdm => mdm.Id, dm => dm.MapFrom(d => d.Director.Id))
                .ForMember(mdm => mdm.FirstName, dm => dm.MapFrom(d => d.Director.FirstName))
                .ForMember(mdm => mdm.LastName, dm => dm.MapFrom(d => d.Director.LastName))
                .ReverseMap();
            CreateMap<GenreMovie, MovieGenresModel>()
                .ForMember(mgm => mgm.Id, gm => gm.MapFrom(g => g.Genre.Id))
                .ForMember(mgm => mgm.Name, gm => gm.MapFrom(g => g.Genre.Name))
                .ForMember(mgm => mgm.Description, gm => gm.MapFrom(g => g.Genre.Description))
                .ReverseMap();
            CreateMap<MediaTypeMovie, MovieMediaTypesModel>()
                .ForMember(mmtm => mmtm.Id, mtm => mtm.MapFrom(mt => mt.MediaType.Id))
                .ForMember(mmtm => mmtm.Name, mtm => mtm.MapFrom(mt => mt.MediaType.Name))
                .ForMember(mmtm => mmtm.Description, mtm => mtm.MapFrom(mt => mt.MediaType.Description))
                .ReverseMap();
            CreateMap<StudioMovie, MovieStudiosModel>()
                .ForMember(msm => msm.Id, sm => sm.MapFrom(s => s.Studio.Id))
                .ForMember(msm => msm.Name, sm => sm.MapFrom(s => s.Studio.Name))
                .ForMember(msm => msm.Description, sm => sm.MapFrom(s => s.Studio.Description))
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