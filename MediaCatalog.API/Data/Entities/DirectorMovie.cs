using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Data.Entities
{
    public class DirectorMovie
    {
        [Required]
        public int DirectorId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Director Director { get; set; }
        public Movie Movie { get; set; }
    }
}