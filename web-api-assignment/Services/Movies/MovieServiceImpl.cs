using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using web_api_assignment.Models.DTOS.Movies;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Utils;

namespace web_api_assignment.Services.Movies
{
    public class MovieServiceImpl : IMovieService
    {
        private readonly WebApiContext _webApiContext;

        public MovieServiceImpl(WebApiContext context)
        {
            _webApiContext = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _webApiContext.Movies.Include(c => c.Characters).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _webApiContext.Movies
                .Include(m => m.Franchise)
                .Include(m => m.Characters)
                .FirstAsync();
        }

        public async Task AddAsync(Movie entity)
        {
            await _webApiContext.AddAsync(entity);
            await _webApiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            _webApiContext.Entry(entity).State = EntityState.Modified;
            await _webApiContext.SaveChangesAsync();

        }

        public async Task DeleteByIdAsync(int id)
        {
            var movie = await _webApiContext.Movies.FindAsync(id);

            _webApiContext.Movies.Remove(movie);
            await _webApiContext.SaveChangesAsync();
        }

        public async Task<ICollection<Character>> GetCharactersAsync(int movieId)
        {
            Movie movie = await _webApiContext.Movies.Include(m => m.Characters).Where(m => m.Id == movieId).FirstOrDefaultAsync();
            return movie.Characters.ToList();
        }

        public async Task UpdateCharactersAsync(int[] characterIds, int movieId)
        {
            List<Character> characters = characterIds
                .ToList()
                .Select(cid => _webApiContext.Characters
                .Where(c => c.Id == cid).First())
                .ToList();
            Movie movie = await _webApiContext.Movies
                .Where(m => m.Id == movieId)
                .FirstAsync();
            movie.Characters = characters;
            _webApiContext.Entry(movie).State = EntityState.Modified;
            _webApiContext.SaveChanges();
        }

    }
}
