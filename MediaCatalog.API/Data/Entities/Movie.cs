using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCatalog.API.Data.Entities
{
    public class Movie : BaseEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "The movie title can only be 50 characters long.")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "The short description can only be 255 characters long.")]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
        public int RunTime { get; set; }
        public int ReleaseYear { get; set; }
        public bool Favorite { get; set; }

        public Rating Rating { get; set; }

        public ICollection<ActorMovie> MovieActors { get; set; }
        public ICollection<DirectorMovie> MovieDirectors { get; set; }
        public ICollection<GenreMovie> MovieGenres { get; set; }
        public ICollection<MediaTypeMovie> MovieMediaTypes { get; set; }
        public ICollection<StudioMovie> MovieStudios { get; set; }
    }
}