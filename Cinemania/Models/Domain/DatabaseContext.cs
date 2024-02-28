using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Importing the namespace for ASP.NET Core Identity Entity Framework Core integration
using Microsoft.EntityFrameworkCore; // Importing the namespace for Entity Framework Core

namespace Cinemania.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser> // Defining the DatabaseContext class which inherits from IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // Constructor for DatabaseContext, taking DbContextOptions<DatabaseContext> as a parameter
        }

        public DbSet<Genre> Genre { get; set; } // DbSet for the Genre entity
        public DbSet<MovieGenre> MovieGenre { get; set; } // DbSet for the MovieGenre entity
        public DbSet<Movie> Movie { get; set; } // DbSet for the Movie entity
    }
}
