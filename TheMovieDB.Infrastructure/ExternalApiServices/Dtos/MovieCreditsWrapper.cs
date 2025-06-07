using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class MovieCreditsWrapper
    {

        // from endpoint https://api.themoviedb.org/3/movie/{movie_id}/credits
        // top of this endpoint, then returns lists of cast/crew (seperate dto's)

        [JsonProperty("id")] // The movie ID
        public int Id { get; set; }

        [JsonProperty("cast")] // List of cast members
        public List<MovieCredCastDto> Cast { get; set; }

        [JsonProperty("crew")] // List of crew members (optional in your example)
        public List<MovieCredCrewDto> Crew { get; set; }
    }
}
