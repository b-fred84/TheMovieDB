using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheMovieDB.Infrastructure.ExternalApiServices.Dtos
{
    public class GenreListDto
    {
        public List<MovieGenreDto> MovieGenres { get; set; }

    }
}
