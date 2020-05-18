using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCatalog.API.Models
{
    public class MovieActorsModel
    {
        public int ActorId { get; set; }

        public string ActorFirstName { get; set; }

        public string ActorLastName { get; set; }

        public string ActorFullName
        {
            get
            {
                var fullName = ActorLastName;
                if (string.IsNullOrWhiteSpace(ActorFirstName)) return fullName;
                if (!string.IsNullOrWhiteSpace(fullName))
                {
                    fullName += ", ";
                }
                fullName += ActorFirstName;
                return fullName;
            }
        }
    }
}