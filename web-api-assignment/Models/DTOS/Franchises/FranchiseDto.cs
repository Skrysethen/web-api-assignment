using System.ComponentModel.DataAnnotations;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS.Franchises
{
    public class FranchiseDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        public string? Description { get; set; }

        public List<int> Movies { get; set; } = null!;
    }
}
