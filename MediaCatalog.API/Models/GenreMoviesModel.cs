using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class GenreMoviesModel
    {
        [Required]
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }
        public string MovieShortDescription { get; set; }
    }
}