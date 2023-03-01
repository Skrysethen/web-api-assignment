using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        Task<ICollection<Movie>> GetMoviesAsync(int characterId);
        Task UpdateMoviesAsync(int[] movieIds, int characterId);
    }
}
