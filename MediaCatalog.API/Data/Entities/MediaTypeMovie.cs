using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Data.Entities
{
    public class MediaTypeMovie
    {
        [Required]
        public int MediaTypeId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public MediaType MediaType { get; set; }
        public Movie Movie { get; set; }
    }
}