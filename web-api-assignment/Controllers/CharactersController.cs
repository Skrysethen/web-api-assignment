using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_assignment.Models;
using web_api_assignment.Models.DTOS.Characters;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Characters;
using web_api_assignment.Utils;

namespace web_api_assignment.Controllers
{
    [Route("api/v1/characters")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CharactersController : ControllerBase
    {
        /// <summary>
        /// Controller for characters that uses the character service
        /// Uses automapper to implement DTO mapping 
        /// </summary>

        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        //Constructor
        public CharactersController(IMapper mapper,ICharacterService characterService)
        {
            _characterService = characterService;
            _mapper = mapper;

        }

        /// <summary>
        /// Get all characters from the database.
        /// </summary>
        /// <returns>List of CharacterDtos</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDto>>> GetCharacters()
        {
            return Ok(_mapper.Map<List<CharacterDto>>( await _characterService.GetAllAsync() ));
        }

        /// <summary>
        /// Get a single character, based on its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One CharacterDto</returns>
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

        /// <summary>
        /// Update a single character with new values based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <returns>Returns NoContent if update is successful</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterPutDto character)
        {
            if (id != character.Id)
            {
                return BadRequest();
            }
            try
            {
                await _characterService.UpdateAsync(
                        _mapper.Map<Character>(character)
                    );
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                // Formatting an error code for the exception messages.
                // Using the built in Problem Details.
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
        /// Post a new character object to the databse
        /// </summary>
        /// <param name="characterDto"></param>
        /// <returns>The newly created character</returns>
        [HttpPost]
        public async Task<IActionResult> PostCharacter(CharacterPostDto characterDto)
        {
            Character character = _mapper.Map<Character>(characterDto);
            await _characterService.AddAsync(character);
            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);

        }

    }
}
