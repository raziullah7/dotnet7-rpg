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
        public ActionResult<List<Character>> Get() 
        {
            return Ok(_characterService.GetAllCharacters());
        }

        // we are sending the id parameter via the url
        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id) 
        {
            return Ok(_characterService.GetCharacterById(id));
        }

        // POST methods, create part of CRUD
        // we are sending the parameter newCharacter via the body of the Post method
        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            return Ok(_characterService.AddCharacter(newCharacter));
        }
    }
}