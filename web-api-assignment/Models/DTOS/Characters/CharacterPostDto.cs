using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Characters
{
    public class CharacterPostDto
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string? Alias { get; set; }
        [MaxLength(1)]
        public string? Gender { get; set; }
    }
}
