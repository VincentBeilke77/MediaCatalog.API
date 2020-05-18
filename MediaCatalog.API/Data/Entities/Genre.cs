using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCatalog.API.Data.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(25, ErrorMessage = "Genre name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GenreMovie> GenreMovies { get; set; }
    }
}