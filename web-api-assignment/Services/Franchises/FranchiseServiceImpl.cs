using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models.Entities;
using web_api_assignment.Utils;

namespace web_api_assignment.Services.Franchises
{
    public class FranchiseServiceImpl : IFranchiseService
    {
        private readonly WebApiContext _webApiContext;
        private readonly ILogger<FranchiseServiceImpl> _logger;

        public FranchiseServiceImpl(WebApiContext context, ILogger<FranchiseServiceImpl> logger)
        {
            _webApiContext = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            return await _webApiContext.Franchises.Include(f => f.Movies).ToListAsync();
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            if (!await FranchiseExists(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new EntityNotFoundException();
            }

            return await _webApiContext.Franchises
                .Where(f => f.Id == id)
                .Include(f => f.Movies)
                .FirstAsync();
        }

        public async Task AddAsync(Franchise entity)
        {
            await _webApiContext.AddAsync(entity);
            await _webApiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Franchise entity)
        {
            if (!await FranchiseExists(entity.Id))
            {
                _logger.LogError("Franchise not found with Id: " + entity.Id);
                throw new EntityNotFoundException();
            }

            _webApiContext.Entry(entity).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();

        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await FranchiseExists(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new EntityNotFoundException();
            }

            var franchise = await _webApiContext.Franchises.FindAsync(id);

            _webApiContext.Franchises.Remove(franchise);
            await _webApiContext.SaveChangesAsync();

        }

        
        public async Task<ICollection<Movie>> GetMoviesAsync(int franchiseId)
        {
            return await _webApiContext.Movies
                .Where(f => f.FranchiseId == franchiseId)
                .ToListAsync();
        }

        public async Task UpdateMoviesAsync(int[] movieIds, int franchiseId)
        {
            if (!await FranchiseExists(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new EntityNotFoundException();
            }

            List<Movie> movies = movieIds.ToList().Select(mid => _webApiContext.Movies.Where(m => m.Id == mid).First()).ToList();
            Franchise franchise = await _webApiContext.Franchises.Where(f => f.Id == franchiseId).FirstAsync();
            franchise.Movies = movies;
            _webApiContext.Entry(franchise).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int franchiseId)
        {
            if (!await FranchiseExists(franchiseId))
            {
                _logger.LogError("Franchise not found with Id: " + franchiseId);
                throw new EntityNotFoundException();
            }

            List<Movie> movies = await _webApiContext.Movies.Include(m => m.Characters).Where(f => f.FranchiseId == franchiseId).ToListAsync();
            List<Character> characters = new List<Character>();
            foreach(Movie movie in movies)
            {
                characters.AddRange(movie.Characters.ToList());
            }

            return characters.Distinct().ToList();
        }

        private async Task<bool> FranchiseExists(int id)
        {
            return await _webApiContext.Franchises.AnyAsync(f => f.Id == id);
        }

    }
}
