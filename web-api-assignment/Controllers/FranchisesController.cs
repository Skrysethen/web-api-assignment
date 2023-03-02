using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models.DTOS.Franchises;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Utils;

namespace web_api_assignment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFranchiseService _franchiseService;

        public FranchisesController(IMapper mapper, IFranchiseService franchiseService)
        {
            _mapper = mapper;
            _franchiseService = franchiseService;
        }

        // GET: api/Franchises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseDto>>> GetFranchises()
        {
            return Ok(_mapper.Map<List<FranchiseDto>>(await _franchiseService.GetAllAsync()));
        }

        // GET: api/Franchises/5
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

        // PUT: api/Franchises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, Franchise franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            try
            {
                await _franchiseService.UpdateAsync(franchise);
                return NoContent();

            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails(){Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound)});
            }
        }

        // POST: api/Franchises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(Franchise franchise)
        {
            await _franchiseService.AddAsync(franchise);
            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        }

        // DELETE: api/Franchises/5
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

        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesForFranchiseAsync(int id)
        {
            try
            {
                return Ok(await _franchiseService.GetMoviesAsync(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new ProblemDetails(){Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound)});
            }
        }

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
                return NotFound(new ProblemDetails(){Detail = ex.Message, Status = ((int)HttpStatusCode.NotFound)});
            }
        }

    }
}
