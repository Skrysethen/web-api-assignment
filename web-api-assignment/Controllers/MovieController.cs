using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models.DTOS.Characters;
using web_api_assignment.Models.DTOS.Movies;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Characters;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Services.Movies;
using web_api_assignment.Utils;

namespace web_api_assignment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// Controller for movies that uses the movie service
        /// Uses automapper to implement DTO mapping 
        /// </summary>

        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        //constructor
        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all movies from the database.
        /// </summary>
        /// <returns>List of MovieDtos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            return Ok( _mapper.Map<List<MovieDto>>(await _movieService.GetAllAsync()));
        }

        /// <summary>
        /// Get a single movie, based on its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One MovieDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            try
            {
                return Ok(_mapper.Map<MovieDto>(
                    await _movieService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        /// <summary>
        /// Update a single movie with new values based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns>Returns NoContent if update is successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MoviePutDto movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {

                await _movieService.UpdateAsync(
                    _mapper.Map<Movie>(movie));
                return NoContent();

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        /// <summary>
        /// Post a new movie object to the databse
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns>The newly created movie</returns>
        [HttpPost]
        public async Task<ActionResult> PostMovie(MoviePostDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            await _movieService.AddAsync(movie);
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Delete a movie based on Id
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Get all characters that has belongs to a movie.
        /// A character belongs to a movie when its in the movie's list of characters (many to many relationship)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a list of CharacterDtos</returns>
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharactersForMovieAsync(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterDto>>(await _movieService.GetCharactersAsync(id)));
                
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

        /// <summary>
        /// Update the list of characters in a movie with Id equal to id, based on the list of character ids. 
        /// </summary>
        /// <param name="movieIds"></param>
        /// <param name="id"></param>
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

