using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Core.Enums;

namespace TheMovieDB.Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PlaceOfBirth { get; set; }
        public DateOnly? DOB { get; set; }
        public DateOnly? DateOfDeath { get; set; }
        public Gender? Gender { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; }
    }
}
