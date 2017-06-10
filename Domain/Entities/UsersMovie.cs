using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsersMovie
    {
        [Key]
        public int UserMovieID { get; set; }

        public string UserID { get; set; }

        public int MovieID { get; set; }

        public Byte Rating { get; set; }
    }
}
