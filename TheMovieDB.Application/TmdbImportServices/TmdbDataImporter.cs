using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Core.Models;
using TheMovieDB.Infrastructure.Data;
using TheMovieDB.Infrastructure.ExternalApiServices.ApiClient;
using TheMovieDB.Infrastructure.ExternalApiServices.Dtos;
using TheMovieDB.Infrastructure.ExternalApiServices.Mapping;

namespace TheMovieDB.Application.TmdbImportServices
{
    public class TmdbDataImporter : ITmdbDataImporter
    {
        private readonly TheMovieDbContext _dbContext;
        private readonly ITmdbApiClient _apiClient;

        public TmdbDataImporter(TheMovieDbContext dbContext, ITmdbApiClient apiClient)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
        }


        public async Task ImportGenresAsync()
        {

            try
            {
                var genreWrapper = await _apiClient.GetGenresAsync();

                if (genreWrapper.Genres == null)
                {
                    Console.WriteLine("No GENRES returned from the api");
                    return;
                }

                foreach (var genreDto in genreWrapper.Genres)
                {
                    try
                    {
                        if (!_dbContext.Genres.Any(g => g.Id == genreDto.Id))
                        {
                            Genre genre = MovieMapper.MapTo_Genre(genreDto);
                            _dbContext.Genres.Add(genre);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error using ImportGenresAsync. For gernre Id {genreDto.Id}: {ex.Message}");
                    }

                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportGenresAsync: {ex.Message}");
            }


        }

        #region get movie methods
        //gets one movie page from api by page number (20 movies per page),
        //adds the movies to Movie table and adds the movieId + GenreIds to the MovieGenre table
        public async Task ImportPopularMoviePageAsync(int page)
        {
            try
            {
                var movieWrapper = await _apiClient.GetPopularMoviesPageAsync(page);

                if (movieWrapper.Movies == null)
                {
                    Console.WriteLine("No MOVIES returned from the api");
                    return;
                }

                foreach (var movieDto in movieWrapper.Movies)
                {
                    //added for checking bug - remove when fixed
                    if (movieDto == null)
                    {
                        Console.WriteLine("movieDto is null");
                        continue;
                    }

                    try
                    {
                        if (!_dbContext.Movies.Any(m => m.Id == movieDto.Id))
                        {
                            Movie movie = MovieMapper.MapTo_Movie(movieDto);

                            if (movieDto.GenreIds != null)
                            {
                                foreach (var genreId in movieDto.GenreIds)
                                {
                                    if (!_dbContext.MovieGenres.Any(mg => mg.GenreId == genreId))
                                    {
                                        movie.MovieGenres.Add(MovieMapper.MapTo_MovieGenre(movie.Id, genreId));
                                    }
                                }
                            }

                            await _dbContext.Movies.AddAsync(movie);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error using ImportPopularMoviePageAsync. Failed to import movie Id: {movieDto.Id}: {ex.Message}");
                    }


                }

                await _dbContext.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportPopularMoviePageAsync.  Movie page {page}: {ex.Message}");
            }


        }

        //this method calls a specified number of pages,
        //the api allows 40 requests every 10 seconds so a pause of 250mil secs is needed
        //in order to not break rules and potentially ahve requests blocked(added 300 to be safe)
        public async Task ImportMultipleMoviePagesAsync(int totalPages = 10)
        {
            for (int page = 1; page <= totalPages; page++)
            {
                await ImportPopularMoviePageAsync(page);
                await Task.Delay(300);
            }
        }
        #endregion

        #region get movie credits methods
        //gets the credits for each movie by id, adds the cast (actors) to the Movie actor table
        //gets the director from crew and adds to the MovieDirector table
        //adds Ids for both to the person table,  later these ids will be used to seach person by id
        //and fill in full details for each person
        public async Task ImportMovieCreditsAsync(int movieId)
        {

            try
            {
                var creditsWrapper = await _apiClient.GetMovieCreditsAsync(movieId);

                if (creditsWrapper == null)
                {
                    Console.WriteLine($"No CREDITS returned for movie {movieId}");
                    return;
                }


                foreach (var castDto in creditsWrapper.Cast.Where(c => c != null && c.Order <= 9))
                {
                    try
                    {

                        var person = _dbContext.People.Local.FirstOrDefault(p => p.Id == castDto.Id)
                                     ?? await _dbContext.People.FindAsync(castDto.Id);

                        if (person == null)
                        {
                            person = new Person { Id = castDto.Id };
                            await _dbContext.People.AddAsync(person);
                        }

                        var movieActor = _dbContext.MovieActors.Local.FirstOrDefault(ma => ma.MovieId == movieId && ma.PersonId == castDto.Id)
                                         ?? await _dbContext.MovieActors.FirstOrDefaultAsync(ma => ma.MovieId == movieId && ma.PersonId == castDto.Id);
                                    

                        if (movieActor == null)
                        {
                            MovieActor actor = MovieMapper.MapTo_MovieActor(movieId, castDto.Id);
                            await _dbContext.MovieActors.AddAsync(actor);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error using ImportMovieCreditsAsync. For CAST member {castDto.Id}, for movie {movieId}:  {ex.Message}");
                    }


                }

                foreach (var crewDto in creditsWrapper.Crew.Where(c => c != null && c.Job == "Director"))
                {
                    try
                    {

                        var person = _dbContext.People.Local.FirstOrDefault(p => p.Id == crewDto.Id)
                                     ?? await _dbContext.People.FindAsync(crewDto.Id);

                        if (person == null)
                        {
                            person = new Person { Id = crewDto.Id };
                            await _dbContext.People.AddAsync(person);
                        }

                        var movieDirector = _dbContext.MovieDirectors.Local.FirstOrDefault(md => md.MovieId == movieId && md.PersonId == crewDto.Id)
                                            ?? await _dbContext.MovieDirectors.FirstOrDefaultAsync(md => md.MovieId == movieId && md.PersonId == crewDto.Id);



                        if (movieDirector == null)
                        {
                            MovieDirector director = MovieMapper.MapTo_MovieDirector(movieId, crewDto.Id);
                            await _dbContext.MovieDirectors.AddAsync(director);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error using ImportMovieCreditsAsync. For CREW member {crewDto.Id}, for movie {movieId}:  {ex.Message}");
                    }

                }

                await _dbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportMovieCreditsAsync. For movie {movieId}: {ex.Message}");
            }

        }

        //gets movie ids from the db and loops through and adds each one
        //with a delay to stick to api rules
        public async Task ImportAllMovieCreditsAsync()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportAllMovieCreditsAsync method: {ex.Message}");
            }

        }
        #endregion

        #region Person/people methods
        //gets a person by id, matches to the id in the db and updates the missing info for that person
        public async Task ImportPersonAsync(int id)
        {
            try
            {
                var personDto = await _apiClient.GetPersonAsync(id);

                if (personDto == null)
                {
                    Console.WriteLine($"no PERSON found for id {id}");
                    return;
                }

                Person personModel = MovieMapper.MapTo_Person(personDto);

                Person person = await _dbContext.People.FindAsync(id);

                //should not need an else to insert new as all ids used come from the db already
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportPersonAsync. Person Id: {id}: {ex.Message}");
            }

        }

        //loop through all the people (at this stage  ids only) in the db and calls the method to add missing details
        public async Task ImportAllPeopleAsync()
        {
            try
            {
                var personIds = await _dbContext.People.Select(p => p.Id).ToListAsync();

                if (personIds == null)
                {
                    Console.WriteLine("No PERSON IDs found in DB.");
                    return;
                }

                foreach (var id in personIds)
                {
                    await ImportPersonAsync(id);
                    await Task.Delay(300);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error using ImportAllPeopleAsync: {ex.Message}");
            }
        }

        #endregion
    }
}

