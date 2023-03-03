using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Movies
{
    public class MoviePostDto
    {
        public string MovieTitle { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string Director { get; set; } = null!;
    }
}
