using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheMovieDB.Infrastructure.Data
{
    public class TheMovieDbContextFactory : IDesignTimeDbContextFactory<TheMovieDbContext>
    {

        public TheMovieDbContext CreateDbContext(string[] args)
        {

            

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TheMovieDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("TheMovieDbConnection"));

            return new TheMovieDbContext(optionsBuilder.Options);
        }
    }
}
