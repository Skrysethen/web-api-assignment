using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_assignment.Models.Entities
{
    [Table("Character")]
    public class Character
    {
        /// <summary>
        /// Character entity. Includes a collection of movie objects
        /// </summary>
        public Character() 
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [MaxLength(50)]
        public string? Alias { get; set; }
        [MaxLength(1)]
        public string? Gender { get; set; }
        [MaxLength(200)]
        public string? PictureUrl { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        
    }
}
