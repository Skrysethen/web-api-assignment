using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models.Entities;

namespace web_api_assignment.Services.Franchises
{
    public class FranchiseServiceImpl : IFranchiseService
    {
        private readonly WebApiContext _webApiContext;

        public FranchiseServiceImpl(WebApiContext context)
        {
            _webApiContext = context;
        }

        public async Task<IEnumerable<Franchise>> GetAllAsync()
        {
            return await _webApiContext.Franchises.Include(f => f.Movies).ToListAsync();
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            return await _webApiContext.Franchises
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
            _webApiContext.Entry(entity).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();

        }

        public async Task DeleteByIdAsync(int id)
        {
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
            List<Movie> movies = movieIds.ToList().Select(mid => _webApiContext.Movies.Where(m => m.Id == mid).First()).ToList();
            Franchise franchise = await _webApiContext.Franchises.Where(f => f.Id == franchiseId).FirstAsync();
            franchise.Movies = movies;
            _webApiContext.Entry(franchise).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();
        }

        public Task<ICollection<Character>> GetCharactersAsync(int franchiseId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCharactersAsync(int[] characterIds, int franchiseId)
        {
            throw new NotImplementedException();
        }
    }
}
