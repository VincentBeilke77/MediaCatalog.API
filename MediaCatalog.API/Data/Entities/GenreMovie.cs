using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Data.Entities
{
    public class GenreMovie
    {
        [Required]
        public int GenreId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Genre Genre { get; set; }
        public Movie Movie { get; set; }
    }
}