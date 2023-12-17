using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg_vs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        // fields
        public readonly ICharacterService _characterService;

        // constructor
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        // GET methods, read part of CRUD
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get() 
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        // we are sending the id parameter via the url
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id) 
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        // POST methods, create part of CRUD
        // we are sending the parameter newCharacter via the body of the Post method
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}