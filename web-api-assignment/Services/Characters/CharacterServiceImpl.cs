using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using web_api_assignment.Models.Entities;
using web_api_assignment.Utils;

namespace web_api_assignment.Services.Characters
{
    public class CharacterServiceImpl : ICharacterService
    {
        private readonly WebApiContext _webApiContext;
        private readonly ILogger<CharacterServiceImpl> _logger;

        public CharacterServiceImpl(WebApiContext context, ILogger<CharacterServiceImpl> logger) 
        { 
            _webApiContext = context;
            _logger = logger;
        }
        public async Task AddAsync(Character entity)
        {
            await _webApiContext.Characters.AddAsync(entity);
            await _webApiContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if(!await CharacterExists(id))
            {
                _logger.LogError("Character not found with Id: " + id);
                throw new EntityNotFoundException();
            }

            var franchise = await _webApiContext.Franchises.FindAsync(id);

            _webApiContext.Franchises.Remove(franchise);
            await _webApiContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _webApiContext.Characters.Include(c => c.Movies).ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            if(!await CharacterExists(id))
            {
                _logger.LogError("Character not found with Id: " + id);
                throw new EntityNotFoundException();
            }
            return await _webApiContext.Characters.Where(c => c.Id == id).Include(c => c.Movies).FirstAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int characterId)
        {
            // Log and throw error handling
            if (!await CharacterExists(characterId))
            {
                _logger.LogError("Character not found with Id: " + characterId);
                throw new EntityNotFoundException();
            }

            return (await _webApiContext.Characters
                .Where(c => c.Id == characterId)
                .Include(c => c.Movies)
                .FirstAsync()).Movies;
        }

        public async Task UpdateAsync(Character entity)
        {
            if (!await CharacterExists(entity.Id))
            {
                _logger.LogError("Character not found with Id: " + id);
                throw new EntityNotFoundException();
            }
            _webApiContext.Entry(entity).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();
        }

        public async Task UpdateMoviesAsync(int[] movieIds, int characterId)
        {
            if(!await CharacterExists(characterId))
            {
                _logger.LogError("Character not found with Id: " + characterId);
                throw new EntityNotFoundException();
            }
            List<Movie> movies = movieIds
                .ToList()
                .Select(mid => _webApiContext.Movies
                .Where(m => m.Id == mid).First())
                .ToList();
            // Get character for Id
            Character character = await _webApiContext.Characters
                .Where(c => c.Id == characterId)
                .FirstAsync();
            // Set the character movies
            character.Movies = movies;
            _webApiContext.Entry(character).State = EntityState.Modified;
            // Save all the changes
            _webApiContext.SaveChanges();

        }

        private async Task<bool> CharacterExists(int id)
        {
            return await _webApiContext.Characters.AnyAsync(c => c.Id == id);
        }
    }
}
