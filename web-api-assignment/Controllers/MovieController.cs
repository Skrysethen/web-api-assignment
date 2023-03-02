using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Services.Movies;
using web_api_assignment.Utils;

namespace web_api_assignment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return Ok(await _movieService.GetAllAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                return Ok(await _movieService.GetByIdAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.UpdateAsync(movie);
                return NoContent();

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            await _movieService.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        [HttpGet("{id}/character")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharactersForMovieAsync(int id)
        {
            try
            {
                return Ok(await _movieService.GetCharactersAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        [HttpPut("{id}/character")]
        public async Task<IActionResult> UpdateCharactersForMovieAsync(int[] characterIds, int id)
        {
            try
            {
                await _movieService.UpdateCharactersAsync(characterIds, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }
    }
}

