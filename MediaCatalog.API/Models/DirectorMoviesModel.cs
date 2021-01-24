using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class DirectorMoviesModel
    {
        [Required]
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }
        public string MovieShortDescription { get; set; }
    }
}