using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class MovieTopResponseDto
    {
        //from same endpoints as listed in MovieDto file,  returns a page (up to 500) with 20 movies per page
        //can use this to get whatever amount of movies is desired (up to 10k)

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<MovieDto> Movies { get; set; }
    }
}
