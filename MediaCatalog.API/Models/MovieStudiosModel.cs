using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class MovieStudiosModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Studio name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}