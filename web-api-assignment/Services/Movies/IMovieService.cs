using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Movies
{
    public interface IMovieService: ICrudService<Movie, int>
    {
        Task<ICollection<Character>> GetCharactersAsync(int franchiseId);
        Task UpdateCharactersAsync(int[] characterIds, int franchiseId);
    }
}
