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
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        // GET methods, read part of CRUD
        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get() 
        {
            return Ok(characters);
        }

        // we are sending the id parameter via the url
        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id) 
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }

        // POST methods, create part of CRUD
        // we are sending the parameter newCharacter via the body of the Post method
        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}