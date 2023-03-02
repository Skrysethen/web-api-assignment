using System.ComponentModel.DataAnnotations;

namespace web_api_assignment.Models.DTOS.Franchises
{
    public class FranchisePostDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
