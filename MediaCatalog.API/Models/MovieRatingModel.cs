using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class MovieRatingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}