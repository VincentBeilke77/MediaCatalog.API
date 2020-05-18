using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Data.Entities
{
    public class StudioMovie
    {
        [Required]
        public int StudioId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Studio Studio { get; set; }
        public Movie Movie { get; set; }
    }
}