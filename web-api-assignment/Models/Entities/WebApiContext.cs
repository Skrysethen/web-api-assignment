using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.Entities
{
    /// <summary>
    /// This is where we seed the data for the database.
    /// </summary>
    public partial class WebApiContext : DbContext
    {
        public WebApiContext()
        {
        }
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }

        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Franchises
            builder.Entity<Franchise>().HasData(
                new Franchise() { Id = 1, Name = "Batman", Description = "Nananana batman" },
                new Franchise() { Id = 2, Name = "LotR", Description = "Dudes goes to volcano" },
                new Franchise() { Id = 3, Name = "Harry Potter", Description = "Boy with a scar and round glasses" }
                );

            //Characters
            builder.Entity<Character>().HasData(
                new Character() { Id = 1, FullName = "Robin the sidekick", Alias = "The kid next to batman", Gender = "m"},
                new Character() {  Id = 2, FullName = "Gandalf", Alias = "The gray", Gender = "m"},
                new Character() { Id = 3, FullName = "Hermine", Gender = "f" },
                new Character() { Id = 4, FullName = "Batman", Alias = "Bruce Wayne", Gender = "m"}
                );

            //Movies
            builder.Entity<Movie>().HasData(
                new Movie() { Id = 1, MovieTitle = "Harry Potter and the philosopherstone", Director = "Superman", Genre = "Fantasy" },
                new Movie() { Id = 2, MovieTitle = "Lord of the rings 3", Director = "Some dude", Genre = "Fantasy" },
                new Movie() { Id = 3, MovieTitle = "Batman the dark knight", Director = "Christopher Nolan", Genre = "Superhero"},
                new Movie() {  Id = 4, FranchiseId = 1, MovieTitle = "Batman begins", Director ="Christopher Nolan", Genre = "Superhero"}
                );


            //Seeding data to junktion table
            builder
                .Entity<Movie>()
                .HasMany(mov => mov.Characters)
                .WithMany(chrs => chrs.Movies)
                .UsingEntity(j =>
                    j.HasData(
                        new { MoviesId = 1, CharactersId = 1 },
                        new { MoviesId = 2, CharactersId = 1 },
                        new { MoviesId = 3, CharactersId = 1 },
                        new { MoviesId = 1, CharactersId = 2 },
                        new { MoviesId = 1, CharactersId = 3 },
                        new { MoviesId = 3, CharactersId = 3 },
                        new { MoviesId = 4, CharactersId = 4 }
                    )
                 );
        }

    }
}
