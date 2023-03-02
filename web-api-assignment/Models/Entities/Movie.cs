using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string MovieTitle { get; set; } = null!;
        [MaxLength(50)]
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        public string Director { get; set; } = null!;
        [MaxLength(300)]
        public string PictureURL { get; set; } = null!;
        [MaxLength(300)]
        public string TrailerUrl { get; set; } = null!;

        public int? FranchiseId { get; set; }
        public Franchise Franchise { get; set; } = null!;


        public virtual ICollection<Character> Characters { get; set; } = new HashSet<Character>();
    }
}