using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class MovieGenresModel
    {
        [Required]
        public int GenreId { get; set; }

        public string GenreName { get; set; }
        public string GenreDescription { get; set; }
    }
}