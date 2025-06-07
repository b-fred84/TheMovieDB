using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class MovieCredCastDto
    {
        // from endpoint https://api.themoviedb.org/3/movie/{movie_id}/credits

        //can use billing order to restrict how many actors are sent to the DB
        //probably only top 10 or something otherwise will have a lot of 'bit part' actors listed

        [JsonProperty("id")] // Unique person ID
        public int Id { get; set; }

        [JsonProperty("order")] // Billing order 
        public int Order { get; set; }


        //Could include name but this will be in person table so only Id needed in this table 
        //having name too is just duplicating data unneccessarily
        //[JsonProperty("name")] // Person's name
        //public string Name { get; set; }


        #region other data from this endpoint, not useful for current db set up
        /*
         
        [JsonProperty("known_for_department")] // Department like Acting, Directing (all should be actors but might have director as an acting credit etc, probs best to avoid using this
        public string KnownForDepartment { get; set; }

        [JsonProperty("credit_id")] // Credit record ID
        public string CreditId { get; set; }

        [JsonProperty("original_name")] // Original name (for multilingual names)
        public string OriginalName { get; set; }

        [JsonProperty("popularity")] // Popularity score
        public double Popularity { get; set; }

        [JsonProperty("profile_path")] // Path to profile image
        public string ProfilePath { get; set; }

        [JsonProperty("adult")] // Whether the person is marked as adult
        public bool Adult { get; set; }

        [JsonProperty("cast_id")] // Cast ID (internal TMDb reference)
        public int CastId { get; set; }

        [JsonProperty("gender")] // Gender (by int id)
        public int Gender { get; set; }

        [JsonProperty("character")] // Character name
        public string Character { get; set; }
        */
        #endregion

    }
}
