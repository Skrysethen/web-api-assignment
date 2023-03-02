using System.ComponentModel.DataAnnotations;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS
{
    public class FranchiseDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        public string? Description { get; set; }

        public List<int> Movies { get; set; } = null!;
    }
}
