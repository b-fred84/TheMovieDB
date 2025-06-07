using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class GenreDto
    {
        //from endpoint https://api.themoviedb.org/3/genre/movie/list

        [JsonProperty("id")]           // Unique identifier for the genre
        public int Id { get; set; }

        [JsonProperty("name")]         // Name of the genre (e.g., "Action", "Comedy")
        public string Name { get; set; }
    }
}
