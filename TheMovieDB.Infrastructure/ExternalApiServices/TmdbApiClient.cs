using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Infrastructure.ExternalApiServices.Dtos;
using TheMovieDB.Infrastructure.ExternalApiServices.Settings;
using static System.Net.WebRequestMethods;



namespace TheMovieDB.Infrastructure.ExternalApiServices
{
    public class TmdbApiClient
    {
        private readonly HttpClient _httpClient;

        public TmdbApiClient(HttpClient httpClient, TmdbApiSettings settings)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.AccessToken);
           
        }


        //generic method to get data from TMDB api endpoints
        private async Task<T> GetTmdbApiDataAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch data from url: {url}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        // get 1 page (20 movies) from api by page number
        public async Task<MovieDtoWrapper> GetPopularMoviesPageAsync(int page)
        {
            string url = $"https://api.themoviedb.org/3/movie/popular?page={page}";
            return await GetTmdbApiDataAsync<MovieDtoWrapper>(url);
        }

        // get credits (cast and crew) for a movie by id
        public async Task<MovieCreditsWrapper> GetMovieCreditsAsync(int movieId)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}/credits";
            return await GetTmdbApiDataAsync<MovieCreditsWrapper>(url);
        }

      
        public async Task<GenreWrapper> GetGenresAsync()
        {
            string url = "https://api.themoviedb.org/3/genre/movie/list";
            return await GetTmdbApiDataAsync<GenreWrapper>(url);
        }

        
        public async Task<PersonDto> GetPersonAsync(int personId)
        {
            string url = $"https://api.themoviedb.org/3/person/{personId}";
            return await GetTmdbApiDataAsync<PersonDto>(url);
        }

       
        public async Task<PersonCreditsWrapper> GetPersonMovieCreditsAsync(int personId)
        {
            string url = $"https://api.themoviedb.org/3/person/{personId}/movie_credits";
            return await GetTmdbApiDataAsync<PersonCreditsWrapper>(url);
        }
  
    }
}
