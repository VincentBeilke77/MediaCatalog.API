using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class MovieGenresModel
    {
        public int GenreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}