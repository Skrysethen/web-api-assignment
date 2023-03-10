using System.ComponentModel.DataAnnotations;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS.Movies
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string? Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
        public string PictureURL { get; set; } = null!;
        public string TrailerUrl { get; set; } = null!;
        public int FranchiseId { get; set; }
        public virtual List<int> Characters { get; set; } = null!;
    }
}