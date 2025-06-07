using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class PersonCreditsWrapper
    {
        [JsonProperty("cast")]
        public List<PersonCreditsDto> Cast { get; set; }

        [JsonProperty("crew")]
        public List<PersonCreditsDto> Crew { get; set; }

        [JsonProperty("id")]
        public int PersonId { get; set; }
    }
}
