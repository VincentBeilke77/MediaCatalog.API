﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class MovieMediaTypesModel
    {
        public int MediaTypeId { get; set; }

        [StringLength(25, ErrorMessage = "Media type name can only be 25 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}