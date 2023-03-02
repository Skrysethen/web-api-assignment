namespace web_api_assignment.Models.DTOS.Characters
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public List<int> Movies { get; set; } = null!;
    }
}
