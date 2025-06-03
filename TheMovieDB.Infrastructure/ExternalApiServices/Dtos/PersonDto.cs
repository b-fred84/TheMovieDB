using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class PersonDto
    {

        //from endpoint https://api.themoviedb.org/3/person/{personId}

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")] // Full name
        public string Name { get; set; }

        [JsonProperty("birthday")] // Date of birth (nullable)
        public string DOB { get; set; }

        [JsonProperty("place_of_birth")] // Place of birth, returns region(city, state etc) then country comma seperated
        public string PlaceOfBirth { get; set; }

        [JsonProperty("deathday")] // Date of death (nullable)
        public string DateOfDeath { get; set; }

        [JsonProperty("gender")] // Gender (1 = Female, 2 = Male, 0 = Not specified)
        public int Gender { get; set; }


        //probs not needed
        [JsonProperty("known_for_department")] // Main department (e.g., Acting, Directing)
        public string KnownForDepartment { get; set; }






        #region other data from this endpoint, not useful for current db set up
        //not important
        /*
        [JsonProperty("popularity")] // Popularity score from TMDb
        public double Popularity { get; set; }

        [JsonProperty("profile_path")] // Profile image path (can be appended to image base URL)
        public string ProfilePath { get; set; }

        [JsonProperty("adult")] // Indicates if the person is marked as adult content
        public bool Adult { get; set; }

        [JsonProperty("also_known_as")] // List of alternative names
        public List<string> AlsoKnownAs { get; set; }

        [JsonProperty("biography")] // Full biography
        public string Biography { get; set; }

        [JsonProperty("homepage")] // Personal or official homepage URL
        public string Homepage { get; set; }

        [JsonProperty("imdb_id")] // IMDb ID (for cross-reference)
        public string ImdbId { get; set; }
        */
        #endregion
    }
}
