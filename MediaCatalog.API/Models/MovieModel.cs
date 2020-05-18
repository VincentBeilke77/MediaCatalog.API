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
        public string Name { get; set; }

        public string RatingShortDescription { get; set; }
        public string RatingDescription { get; set; }

        public ICollection<MovieGenresModel> MovieGenres { get; set; }
        public ICollection<MovieActorsModel> MovieActors { get; set; }
    }
}