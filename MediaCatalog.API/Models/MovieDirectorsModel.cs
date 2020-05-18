using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class MovieDirectorsModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25, ErrorMessage = "Last name can only be 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Last name can only be 25 characters.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                var fullName = LastName;
                if (string.IsNullOrWhiteSpace(FirstName)) return fullName;
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    fullName = ", ";
                }
                fullName += FirstName;
                return fullName;
            }
        }
    }
}