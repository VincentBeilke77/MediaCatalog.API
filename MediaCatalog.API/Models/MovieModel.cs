using MediaCatalog.API.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class MovieModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The movie title can only be 50 characters long.")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "The short description can only be 255 characters long.")]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
        public int RunTime { get; set; }
        public int ReleaseYear { get; set; }
        public bool Favorite { get; set; }
        public RatingModel Rating { get; set; }

        public ICollection<MovieGenresModel> MovieGenres { get; set; }
        public ICollection<MovieActorsModel> MovieActors { get; set; }
        public ICollection<MovieDirectorsModel> MovieDirectors { get; set; }
        public ICollection<MovieMediaTypesModel> MovieMediaTypes { get; set; }
        public ICollection<MovieStudiosModel> MovieStudios { get; set; }
    }
}