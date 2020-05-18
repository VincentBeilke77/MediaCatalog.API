using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCatalog.API.Data.Entities
{
    public class Studio : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Studio name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<StudioMovie> StudioMovies { get; set; }
    }
}