using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS.Characters
{
    public class CharacterPutDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
    }
}
