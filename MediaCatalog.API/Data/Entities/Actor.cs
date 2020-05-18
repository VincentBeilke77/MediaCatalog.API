using MediaCatalog.API.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCatalog.API.Data.Entities
{
    public class Actor : BaseEntity
    {
        [MaxLength(25, ErrorMessage = "First name can only be 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Last name can only be 25 characters.")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                var fullName = LastName;
                if (string.IsNullOrWhiteSpace(FirstName)) return fullName;
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    fullName += ", ";
                }
                fullName += FirstName;
                return fullName;
            }
        }

        public ICollection<ActorMovie> Movies { get; set; }
    }
}