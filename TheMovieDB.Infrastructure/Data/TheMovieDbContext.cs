using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDB.Core.Models;

namespace TheMovieDB.Infrastructure.Data
{
    public class TheMovieDbContext : DbContext
    {
        public TheMovieDbContext(DbContextOptions<TheMovieDbContext> options)
            : base(options)
        {
        }



        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(ma => new { ma.MovieId, ma.PersonId });

            modelBuilder.Entity<MovieDirector>().HasKey(md => new { md.MovieId, md.PersonId });

            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId});
        }
    }
}
