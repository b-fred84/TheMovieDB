using TheMovieDB.Infrastructure.ExternalApiServices.Dtos;

namespace TheMovieDB.Infrastructure.ExternalApiServices.ApiClient
{
    public interface ITmdbApiClient
    {
        Task<GenreWrapper> GetGenresAsync();
        Task<MovieCreditsWrapper> GetMovieCreditsAsync(int movieId);
        Task<PersonDto> GetPersonAsync(int personId);
        Task<PersonCreditsWrapper> GetPersonMovieCreditsAsync(int personId);
        Task<MovieDtoWrapper> GetPopularMoviesPageAsync(int page);
    }
}