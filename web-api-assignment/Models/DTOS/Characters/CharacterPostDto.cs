using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Characters
{
    public class CharacterPostDto
    { 
        public string FullName { get; set; }
        public string? Alias { get; set; }
        public string? Gender { get; set; }
    }
}
