using System.ComponentModel.DataAnnotations;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS.Movies
{
    public class MovieDto
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
        public int FranchiseId { get; set; }
        public virtual List<int> Characters { get; set; } = null!;
    }
}