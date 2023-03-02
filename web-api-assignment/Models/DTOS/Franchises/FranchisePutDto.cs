using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Franchises
{
    public class FranchisePutDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
