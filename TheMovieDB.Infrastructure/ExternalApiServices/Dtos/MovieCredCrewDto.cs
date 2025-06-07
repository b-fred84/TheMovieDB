using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class MovieCredCrewDto
    {
        //only crew member needed for DB spec is director, can ignore all others
        // from endpoint https://api.themoviedb.org/3/movie/{movie_id}/credits
        //returns full crew - when mapping to db model only inclue directors (using job or dept)

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("department")] //alternative to using "job" could use department directing
        public string Department { get; set; }

        [JsonProperty("job")]  //use job to get "Director"
        public string Job { get; set; }


        //name shouldnt be needed, just Id (maybe useful in development to verify data easily?)
        //[JsonProperty("name")]
        //public string Name { get; set; }



        #region other data from this endpoint, not useful for current db set up
        /*
        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("known_for_department")] //dont use this as it could have people known for directing who are producers etc on this movie and not director
        public string KnownForDepartment { get; set; }
        */
        #endregion
    }
}
