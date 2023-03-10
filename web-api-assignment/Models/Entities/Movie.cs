using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_assignment.Models.Entities
{
    [Table("Movie")]
    public class Movie
    {
        /// <summary>
        /// Movie entity.
        /// Has a foreign key franchiseId that refers to a franchise, and a collection of character objects
        /// </summary>
        public Movie()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        [MaxLength(50)]
        public string MovieTitle { get; set; } = null!;
        [MaxLength(50)]
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        public string Director { get; set; } = null!;
        [MaxLength(300)]
        public string? PictureURL { get; set; } = null!;
        [MaxLength(300)]
        public string? TrailerUrl { get; set; } = null!;

        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; } = null!;

        public virtual ICollection<Character> Characters { get; set; }
    }
}