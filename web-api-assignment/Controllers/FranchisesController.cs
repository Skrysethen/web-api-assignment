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
using web_api_assignment.Models.DTOS.Franchises;
using web_api_assignment.Models.DTOS.Movies;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Utils;

namespace web_api_assignment.Controllers
{
    /// <summary>
    /// Controller for franchises that uses the franchise service
    /// Uses automapper to implement DTO mapping 
    /// </summary>

    [Route("api/v1/franchise")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        //Constructor
        public FranchisesController(IMapper mapper, IFranchiseService franchiseService)
        {
            _mapper = mapper;
            _franchiseService = franchiseService;
        }

        /// <summary>
        /// Get all franchises from the database.
        /// </summary>
        /// <returns>List of FranchiseDtos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            return Ok(_mapper.Map<List<FranchiseDto>>(await _franchiseService.GetAllAsync()));
        }

        /// <summary>
        /// Get a single franchise, based on its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One FranchiseDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchise(int id)
        {
            try
            {
                return Ok(_mapper.Map<FranchiseDto>(await _franchiseService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }

        }

        /// <summary>
        /// Update a single franchise with new values based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        /// <returns>Returns NoContent if update is successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchisePutDto franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            try
            {
                await _franchiseService.UpdateAsync(_mapper.Map<Franchise>(franchise));
                return NoContent();

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails(){Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound)});
            }
        }

        /// <summary>
        /// Post a new franchise object to the databse
        /// </summary>
        /// <param name="franchiseDto"></param>
        /// <returns>The newly created franchise</returns>
        [HttpPost]
        public async Task<IActionResult> PostFranchise(FranchisePostDto franchiseDto)
        {
            Franchise franchise = _mapper.Map<Franchise>(franchiseDto);
            await _franchiseService.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);

        }

        /// <summary>
        /// Delete a franchise based on Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            try
            {
                await _franchiseService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails(){Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound)});
            }
        }

        /// <summary>
        /// Get all movies that has belongs to a franchise.
        /// A movie belongs to a franchise when its foreign key franchiseId is equal to the Id of the franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a list of movieDtos</returns>

        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMoviesForFranchiseAsync(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<MovieDto>>(await _franchiseService.GetMoviesAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(

                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }
        }

        /// <summary>
        /// Update the list of movies in a franchise with Id equal to id, based on the list of movie ids. 
        /// </summary>
        /// <param name="movieIds"></param>
        /// <param name="id"></param>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesForFranchisesAsync(int[] movieIds, int id)
        {
            try
            {
                await _franchiseService.UpdateMoviesAsync(movieIds, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(
                    new ProblemDetails()
                    {
                        Detail = ex.Message,
                        Status = ((int)HttpStatusCode.NotFound)
                    }
                    );
            }

        }

        /// <summary>
        /// Get all characters that are in movies that belong to the franchise with Id equal to id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of characterDto</returns>
        [HttpGet("{id}/character")]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharactersForFranchiseAsync(int id)
        {
            try
            {
                return Ok(_mapper.Map<List<CharacterDto>>(await _franchiseService.GetCharactersAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails() { Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound) });
            }
        }

    }
}
