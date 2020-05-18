using System.ComponentModel.DataAnnotations;

namespace MediaCatalog.API.Data.Entities
{
    public class ActorMovie
    {
        [Required]
        public int ActorId { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}