using MediaCatalog.API.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class GenreModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Genre name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<GenreMoviesModel> GenreMovies { get; set; }
    }
}