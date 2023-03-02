namespace web_api_assignment.Models.DTOS
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string PictureUrl { get; set; }
        public List<int> Movies { get; set; }
    }
}
