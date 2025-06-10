using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheMovieDB.Infrastructure.Data;
using TheMovieDB.Infrastructure.ExternalApiServices;
using TheMovieDB.Infrastructure.ExternalApiServices.Settings;


var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

//set up services DI
var services = new ServiceCollection();


services.AddDbContext<TheMovieDbContext>(options =>
options.UseSqlServer(config.GetConnectionString("TheMovieDbConnection")));

var tmdbSettings = config.GetSection("TmdbApi").Get<TmdbApiSettings>();
services.AddSingleton(tmdbSettings);

services.AddSingleton<IConfiguration>(config);
services.AddScoped<TmdbApiClient>();


//var apiClient = new TmdbApiClient(new HttpClient(), tmdbSettings);

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TheMovieDbContext>();
    var tmdbApiClient = scope.ServiceProvider.GetRequiredService<TmdbApiClient>();

}




#region testing api returns

/*

// MOVIES

var movieResult = await apiClient.GetPopularMoviesPageAsync(1);
int firstMovieId = movieResult.Movies[0].Id;
Console.WriteLine(firstMovieId);

//Console.WriteLine($"Fetched {movieResult.Movies.Count} movies.");

//foreach (var movie in movieResult.Movies)
//{
//    Console.WriteLine($"Title: {movie.Title}");
//    Console.WriteLine($"Id: {movie.Id}");
//    Console.WriteLine($"Release date: {movie.ReleaseDate}");
//    Console.WriteLine($"Vote count: {movie.VoteCount}");
//    Console.WriteLine($"Vote average: {movie.VoteAverage}");
//    Console.WriteLine($"Popularity: {movie.Popularity}");
//    Console.WriteLine($"Genre Ids: {string.Join(", ", movie.GenreIds)}");
//    Console.WriteLine(new string('-', 22));
//    Console.WriteLine();
//}


//MOVIE CREDITS

var movieCredits = await apiClient.GetMovieCreditsAsync(firstMovieId);
int personId = movieCredits.Cast[0].Id;
Console.WriteLine(personId);

//Console.WriteLine("Cast:");
//foreach (var castMember in movieCredits.Cast)
//{
//    Console.WriteLine($"Cast Id: {castMember.Id},  Order: {castMember.Order}");
//}

//Console.WriteLine("Crew:");
//foreach (var crewMember in movieCredits.Crew)
//{
//    Console.WriteLine($"Crew Id: {crewMember.Id},  Job: {crewMember.Job},   Department: {crewMember.Department}");
//}


// PERSON

var person = await apiClient.GetPersonAsync(personId);

//Console.WriteLine($"Id: {person.Id}");
//Console.WriteLine($"Name: {person.Name}");
//Console.WriteLine($"Date of Birth: {person.DOB}");
//Console.WriteLine($"Place of Birth: {person.PlaceOfBirth}");
//Console.WriteLine($"Date of Death: {person.DateOfDeath ?? "N/A"}");
//Console.WriteLine($"Gender: {person.Gender}");

//PersonCredits
var personCredits = await apiClient.GetPersonMovieCreditsAsync(personId);

//foreach (var credit in personCredits.Cast)  
//{
//    Console.WriteLine($"Id: {credit.Id}");
//    Console.WriteLine($"Title: {credit.Title}");
//    Console.WriteLine($"Popularity: {credit.Popularity}");
//    Console.WriteLine($"Release Date: {credit.ReleaseDate}");
//    Console.WriteLine($"Genre Ids: {string.Join(", ", credit.GenreIds)}");
//    Console.WriteLine("---------------------------");
//}

//foreach (var credit in personCredits.Crew)  
//{
//    Console.WriteLine($"Id: {credit.Id}");
//    Console.WriteLine($"Title: {credit.Title}");
//    Console.WriteLine($"Popularity: {credit.Popularity}");
//    Console.WriteLine($"Release Date: {credit.ReleaseDate}");
//    Console.WriteLine($"Genre Ids: {string.Join(", ", credit.GenreIds)}");
//    Console.WriteLine("---------------------------");
//}

//GENRES
var genreResult = await apiClient.GetGenresAsync();

//foreach (var genre in genreResult.Genres)
//{
//    Console.WriteLine($"- {genre.Id}: {genre.Name}");
//}


*/


#endregion