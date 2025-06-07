using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Core.Models
{
    public class Movie
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public List<int> GenreIds { get; set; }
        public double Popularity { get; set; }
        public double AverageRating { get; set; }
        public int VoteCount { get; set; }


    }
}
