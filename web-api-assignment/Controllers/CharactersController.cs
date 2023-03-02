using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models;
using web_api_assignment.Models.DTOS.Characters;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Characters;

namespace web_api_assignment.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharactersController(IMapper mapper,ICharacterService characterService)
        {
            _characterService = characterService;
            _mapper = mapper;

        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            return Ok(_mapper.Map<CharacterDto>(
                 await _characterService.GetAllAsync()
                ));
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
        {
            try
            {
                return Ok(_mapper.Map<CharacterDto>(
                    await _characterService.GetByIdAsync(id))
                    );
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }

            await _characterService.UpdateAsync(character);

            return NoContent();
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            await _characterService.AddAsync(character);

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

    }
}
