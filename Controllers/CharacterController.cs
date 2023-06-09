using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterServer;

        public CharacterController(ICharacterService characterService)
        {
            _characterServer = characterService;
        }

        [HttpDelete]
        public  async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteCharacter(int Id){
            return await _characterServer.RemoveCharacterById(Id);
        }

        [HttpPut]
        public  async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter){
            return await _characterServer.UpdateCharacter(updatedCharacter);
        }

        [HttpGet("GetAllCharacter")]
        public  async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> GetAllCharacter()
        {
            return Ok(await _characterServer.GetAllCharacter());
        }

        [HttpGet("GetSingle/{id}")]
        public  async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
        {
            try{
                return Ok(await _characterServer.GetCharacterById(id));
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public  async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            return Ok(await _characterServer.AddCharacter(newCharacter));
        }
    }
}