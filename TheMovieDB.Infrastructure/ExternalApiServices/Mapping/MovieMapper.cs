using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Core.Enums;
using TheMovieDB.Core.Models;
using TheMovieDB.Infrastructure.ExternalApiServices.Dtos;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Mapping
{
    public class MovieMapper
    {

        public static Movie MapTo_Movie(MovieDto movieDto)
        {
            if(movieDto == null)
            {
                Console.WriteLine("movieDto is null");
                return null;
            }
            
            DateOnly? releaseDate = null;

            if (DateOnly.TryParse(movieDto.ReleaseDate, out DateOnly date))
            {
                releaseDate = date;
            }
            else
            {
                Console.WriteLine($"Could not parse date for: {movieDto.Title}");
            }

            return new Movie
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                ReleaseDate = releaseDate,
                Popularity = movieDto.Popularity,
                VoteCount = movieDto.VoteCount,
                AverageRating = movieDto.VoteAverage
                

            };

           
            
        }

        public static Person MapTo_Person(PersonDto personDto)
        {
            if (personDto == null)
            {
                Console.WriteLine("personDto is null");
                return null;
            }

            DateOnly? dateOfBirth = null;
            DateOnly? dateOfDeath = null;

            if(DateOnly.TryParse(personDto.DOB, out DateOnly birthDate))
            {
                dateOfBirth = birthDate;
            }
            else
            {
                Console.WriteLine($"Could not parse DOB for: {personDto.Name}");
            }

            if (DateOnly.TryParse(personDto.DateOfDeath, out DateOnly deathDate))
            {
                dateOfDeath = deathDate;
            }


            return new Person
            {
                Id = personDto.Id,
                Name = personDto.Name,
                PlaceOfBirth = personDto.PlaceOfBirth,
                DOB = dateOfBirth,
                DateOfDeath = dateOfDeath,
                Gender = (Gender)personDto.Gender

            };
        }

        public static Genre MapTo_Genre(GenreDto genreDto)
        {
            return new Genre
            {
                Id = genreDto.Id,
                Name = genreDto.Name
            };
        }

        public static MovieDirector MapTo_MovieDirector(int movieId, int personId)
        {
            return new MovieDirector
            {
                MovieId = movieId,
                PersonId = personId
            };
        }

        public static MovieActor MapTo_MovieActor(int movieId, int personId)
        {
            return new MovieActor
            {
                MovieId = movieId,
                PersonId = personId
            };
        }
    }
}
