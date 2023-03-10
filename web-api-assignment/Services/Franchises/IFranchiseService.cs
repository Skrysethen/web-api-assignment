using web_api_assignment.Models;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Franchises
{
    public interface IFranchiseService:ICrudService<Franchise, int>
    {
        Task<ICollection<Movie>> GetMoviesAsync(int franchiseId);
        Task UpdateMoviesAsync(int[] movieIds, int franchiseId);
        Task<ICollection<Character>> GetCharactersAsync(int franchiseId);

    }
}
