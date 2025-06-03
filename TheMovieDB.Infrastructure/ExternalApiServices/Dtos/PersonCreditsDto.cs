using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class PersonCreditsDto
    {
        //from endpoint https://api.themoviedb.org/3/person/{person_id}/combined_credits
        //probably won't use this endpoint (see notes), but useful to have if needed.
        //there are also endpoints that return Person credits for just Movie or Tv rather than combined

        #region Notes 
        // For this project it asks for a list of popular movies,
        // i will probably just use the DB to check which of those movies an actor has been in rather than full list
        // if required to get all then i could use this endpoint,  but then there are issues that arise,
        // should i then just store the id and name of the movie
        // or should i just store the movie id and use that to loop thru and get
        // the movie details and add it to the Movie table - (if it is outside of the original group of most popular movies).
        // Doing this will lead to more movies to collect cast for
        // and then more credits for those cast -> more mnovies
        // (and the loop continues until the DB is way bigger than this project really needs)
        #endregion


        //if using this endpoint these should be only props needed (and maybe just Id depending on structure)

        [JsonProperty("id")]                 // Unique identifier for the movie/show
        public int Id { get; set; }

        [JsonProperty("title")]              // Display title (for movies)
        public string Title { get; set; }

        [JsonProperty("popularity")]         // Popularity score
        public double Popularity { get; set; }

        [JsonProperty("release_date")]       // Release date of the movie/show
        public string ReleaseDate { get; set; }

        [JsonProperty("genre_ids")]          // List of genre IDs associated with the title
        public List<int> GenreIds { get; set; }




        #region  Not needed
        /*
        [JsonProperty("adult")]              // Indicates if the movie/show is for adults
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]      // Path to the backdrop image
        public string BackdropPath { get; set; }

        [JsonProperty("video")]              // Indicates if it's a video release
        public bool Video { get; set; }

        [JsonProperty("vote_average")]       // Average vote score
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]         // Number of votes received
        public int VoteCount { get; set; }

        [JsonProperty("character")]          // Character played by the person
        public string Character { get; set; }

        [JsonProperty("credit_id")]          // Unique credit identifier
        public string CreditId { get; set; }

        [JsonProperty("order")]              // Billing order for the cast
        public int Order { get; set; }

        [JsonProperty("media_type")]         // Type of media: "movie" or "tv"
        public string MediaType { get; set; }

        [JsonProperty("poster_path")]        // Path to the poster image
        public string PosterPath { get; set; }

        [JsonProperty("original_language")]  // Original language of the title
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]     // Original title of the movie (for movies)
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]           // Brief summary of the plot
        public string Overview { get; set; }
        */
        #endregion


    }
}
