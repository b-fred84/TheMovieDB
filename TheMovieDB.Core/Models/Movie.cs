using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double Popularity { get; set; }
        public double AverageRating { get; set; }
        public int VoteCount { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set;}
    }
}
