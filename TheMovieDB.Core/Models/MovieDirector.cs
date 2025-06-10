using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Core.Models
{
    public class MovieDirector
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public Movie Movie { get; set; }
        public Person Person { get; set; }

    }
}
