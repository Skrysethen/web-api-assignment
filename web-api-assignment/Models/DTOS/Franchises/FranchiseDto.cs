using System.ComponentModel.DataAnnotations;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Models.DTOS.Franchises
{
    public class FranchiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> Movies { get; set; } = null!;
    }
}
