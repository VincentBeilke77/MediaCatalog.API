using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCatalog.API.Data.Entities
{
    public class MediaImage : BaseEntity
    {
        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "First name can only be 25 characters.")]
        public string ImageName { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}