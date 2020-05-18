using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCatalog.API.Data.Entities
{
    public class MediaType : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Media type name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<MediaTypeMovie> MediaTypeMovies { get; set; }
    }
}