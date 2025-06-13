
namespace TheMovieDB.Application.TmdbImportServices
{
    public interface ITmdbDataImporter
    {
        Task ImportAllMovieCreditsAsync();
        Task ImportAllPeopleAsync();
        Task ImportGenresAsync();
        Task ImportMovieCreditsAsync(int movieId);
        Task ImportMultipleMoviePagesAsync(int totalPages = 100);
        Task ImportPersonAsync(int id);
        Task ImportPopularMoviePageAsync(int page);
    }
}