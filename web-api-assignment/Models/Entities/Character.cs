using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.Entities
{
    public class Character
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [MaxLength(50)]
        public string? Alias { get; set; }
        [MaxLength(1)]
        public string? Gender { get; set; }
        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
