using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Franchises
{
    public class FranchisePutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
