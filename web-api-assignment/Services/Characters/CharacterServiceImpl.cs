using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Characters
{
    public class CharacterServiceImpl : ICharacterService
    {
        private readonly WebApiContext _context;

        public CharacterServiceImpl(WebApiContext context) { _context = context; }
        public async Task AddAsync(Character entity)
        {
            await _context.Characters.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return await _context.Characters.Where(c => c.Id == id).Include(c => c.Movies).FirstAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int characterId)
        {
            // Log and throw error handling
            if (!await CharacterExists(characterId))
            {
                //_logger.LogError("Professor not found with Id: " + professorId);
                //throw new ProfessorNotFoundException();
            }

            return (await _context.Characters
                .Where(c => c.Id == characterId)
                .Include(c => c.Movies)
                .FirstAsync()).Movies;
        }

        public async Task UpdateAsync(Character entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMoviesAsync(int[] movieIds, int characterId)
        {
            if(!await CharacterExists(characterId))
            {
                //Logging errors 
            }
            List<Movie> movies = movieIds
                .ToList()
                .Select(mid => _context.Movies
                .Where(m => m.Id == mid).First())
                .ToList();
            // Get character for Id
            Character character = await _context.Characters
                .Where(c => c.Id == characterId)
                .FirstAsync();
            // Set the character movies
            character.Movies = movies;
            _context.Entry(character).State = EntityState.Modified;
            // Save all the changes
            _context.SaveChanges();

        }

        private async Task<bool> CharacterExists(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }
    }
}
