using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class MovieDto
    {

        //notes
        //from endpoints https://
        // api.themoviedb.org/3/discover/movie
        // api.themoviedb.org/3/movie/popular
        // api.themoviedb.org/3/movie/top_rated
        //any of these return the same just different ways of ordering movies.  
        //discover has most flexibility as can ask it to return a list by year, actor, genre etc
        //all lists go up to 500 pages max,  with 20 on a page,  so can return 10k max per list type
        //probably use the /popular endpoint as the brief just asks for 'popular' movies and doesn't specify in what way popular (by tmdb popularity, by ratings/total votes etc)


        [JsonProperty("id")] //movie id
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("genre_ids")] //list of ids for any related genres
        public List<int> GenreIds { get; set; }

        [JsonProperty("popularity")] //the movies popularity (tmdb popularity score)
        public double Popularity { get; set; }

        [JsonProperty("vote_average")] //movie rating (by user votes)
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")] //amount of votes the users had
        public int VoteCount { get; set; }



        #region endpoint responses that are either not useful or needed in current state of project 
        /*
        //Not very useful 

        [JsonProperty("video")] //not sure  - not important anyway
        public bool Video { get; set; }

        [JsonProperty("overview")] //description of the movie
        public string Overview { get; set; }

        [JsonProperty("adult")] //is for adults or kids
        public bool Adult { get; set; }

        [JsonProperty("original_title")] //movies original title (for example its name in spanish if its a spanish movie)
        public string OriginalTitle { get; set; }

        



        //Images not useful for backend project

        [JsonProperty("backdrop_path")] //just a jpg/url to get an image for background
        public string BackdropPath { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        */
        #endregion

    }
}
