using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Models
{
    public class ActorModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(25, ErrorMessage = "First name can only be 25 characters.")]
        public string FirstName { get; set; }

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
    }
}