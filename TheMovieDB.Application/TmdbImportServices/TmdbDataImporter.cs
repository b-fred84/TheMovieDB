using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Core.Models;
using TheMovieDB.Infrastructure.Data;
using TheMovieDB.Infrastructure.ExternalApiServices;
using TheMovieDB.Infrastructure.ExternalApiServices.Dtos;
using TheMovieDB.Infrastructure.ExternalApiServices.Mapping;

namespace TheMovieDB.Application.TmdbImportServices
{
    public class TmdbDataImporter
    {
        private readonly TheMovieDbContext _dbContext;
        private readonly TmdbApiClient _apiClient;

        public TmdbDataImporter(TheMovieDbContext dbContext, TmdbApiClient apiClient)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
        }


        public async Task ImportGenresAsync()
        {
            var genreWrapper = await _apiClient.GetGenresAsync();

            if (genreWrapper.Genres == null)
            {
                Console.WriteLine("No GENRES returned from the api");
                return;
            }

            foreach (var genreDto in genreWrapper.Genres)
            {
                if (!_dbContext.Genres.Any(g => g.Id == genreDto.Id))
                {
                    Genre genre = MovieMapper.MapTo_Genre(genreDto);
                    _dbContext.Genres.Add(genre);
                }
            }

            await _dbContext.SaveChangesAsync();
    
        }

        //gets one movie page from api by page number (20 movies per page),
        //adds the movies to Movie table and adds the movieId + GenreIds to the MovieGenre table
        public async Task ImportPopularMoviePageAsync(int page)
        {
            var movieWrapper = await _apiClient.GetPopularMoviesPageAsync(page);

            if(movieWrapper.Movies == null)
            {
                Console.WriteLine("No MOVIES returned from the api");
                return;
            }

            foreach(var movieDto in movieWrapper.Movies)
            {
                if(!_dbContext.Movies.Any(m => m.Id == movieDto.Id))
                {
                    Movie movie = MovieMapper.MapTo_Movie(movieDto);
                    
                    if (movieDto.GenreIds != null)
                    {
                        foreach(var genreId in movieDto.GenreIds)
                        {
                            if(!_dbContext.MovieGenres.Any(mg => mg.GenreId == genreId))
                            {
                                movie.MovieGenres.Add(MovieMapper.MapTo_MovieGenre(movie.Id, genreId));
                            }
                        }
                    }

                    _dbContext.Movies.Add(movie);
                }
                
            }

        }

        //this method calls a specified number of pages,
        //the api allows 40 requests every 10 seconds so a pause of 250mil secs is needed
        //in order to not break rules and potentially ahve requests blocked(added 300 to be safe)
        public async Task ImportMultipleMoviePagesAsync(int totalPages = 100)
        {
            for (int page = 1; page <= totalPages; page++)
            {
                await ImportPopularMoviePageAsync(page);
                await Task.Delay(300);
            }
        }


        public async Task ImportMovieCreditsAsync(int movieId)
        {
            var creditsWrapper = await _apiClient.GetMovieCreditsAsync(movieId);

            if (creditsWrapper == null)
            {
                Console.WriteLine($"No CREDITS returned for movie {movieId}");
                return;
            }


            foreach (var castDto in creditsWrapper.Cast.Where(c => c != null && c.Order <= 9))
            {
               
                if(!await _dbContext.People.AnyAsync(p => p.Id == castDto.Id))
                {
                    Person person = new Person { Id = castDto.Id };
                    await _dbContext.People.AddAsync(person);
                }

                if(!await _dbContext.MovieActors.AnyAsync(ma => ma.MovieId == movieId && ma.PersonId == castDto.Id))
                {
                    MovieActor actor = MovieMapper.MapTo_MovieActor(movieId, castDto.Id);
                    await _dbContext.MovieActors.AddAsync(actor);
                }   
                
            }

            foreach (var crewDto in creditsWrapper.Crew.Where(c => c != null && c.Job == "Director"))
            {
                if (!await _dbContext.People.AnyAsync(p => p.Id == crewDto.Id))
                {
                    Person person = new Person { Id = crewDto.Id };
                    await _dbContext.People.AddAsync(person);
                }

                if(!await _dbContext.MovieDirectors.AnyAsync(md => md.MovieId == movieId && md.PersonId == crewDto.Id))
                {
                    MovieDirector director = MovieMapper.MapTo_MovieDirector(movieId, crewDto.Id);
                    await _dbContext.MovieDirectors.AddAsync(director);
                }

            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task ImportAllMovieCreditsAsync()
        {
            var movieIds = await _dbContext.Movies.Select(m => m.Id).ToListAsync();

            if (movieIds == null)
            {
                Console.WriteLine("No MOVIE IDs found in DB.");
                return;
            }

            foreach (var id in movieIds)
            {
                await ImportMovieCreditsAsync(id);
                await Task.Delay(300);
            }
        }

        public async Task ImportPersonAsync(int id)
        {
            var personDto = await _apiClient.GetPersonAsync(id);
            if (personDto == null)
            {
                Console.WriteLine($"no PERSON found for id {id}");
            }

            Person personModel = MovieMapper.MapTo_Person(personDto);

            Person person = await _dbContext.People.FindAsync(id);

            if (person != null)
            {
                person.Name = personModel.Name;
                person.DOB = personModel.DOB;
                person.DateOfDeath = personModel.DateOfDeath;
                person.PlaceOfBirth = personModel.PlaceOfBirth;
                person.Gender = personModel.Gender;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
