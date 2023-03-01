using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models;

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
                new Character() { Id = 3, FullName = "Hermine", Gender = "f" }
                );

            //Movies
            builder.Entity<Movie>().HasData(
                new Movie() { Id = 1, MovieTitle = "Harry Potter and the philosopherstone", Director = "Superman", Genre = "Fantasy" },
                new Movie() { Id = 2, MovieTitle = "Lord of the rings 3", Director = "Some dude", Genre = "Fantasy" },
                new Movie() { Id = 3, MovieTitle = "Batman the dark knight", Director ="who knows", Genre = "Superhero"}
                );
                
                


            builder.Entity<Movie>()
                .HasMany(mov => mov.Characters)
                .WithMany(chrs => chrs.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCharacter",
                    left => left.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    right => right.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    jt =>
                    {
                        jt.HasKey("MovieId", "CharacterId");
                        jt.HasData(
                            new { MovieId = 1, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 1 },
                            new { MovieId = 2, CharacterId = 2 },
                            new { MovieId = 2, CharacterId = 3 },
                            new { MovieId = 3, CharacterId = 2 }
                            );
                    }
                );

        }

        public virtual DbSet<Franchise> Franchises { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Character> Characters { get; set; }

    }
}
