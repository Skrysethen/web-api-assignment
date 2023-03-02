using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Movies
{
    public class MoviePostDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string MovieTitle { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
    }
}
