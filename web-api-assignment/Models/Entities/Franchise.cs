using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_assignment.Models.Entities
{
    [Table("Franchise")]
    public class Franchise
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<Movie> Movies { get; set; } = null!;

    }
}