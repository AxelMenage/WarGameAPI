using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarGameAPI.Entities;
using WarGameAPI.Entities.Views;
using WarGameAPI.Models;
using WarGameAPI.Services;

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Game
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<string> Get(int id)
        {
            GamesView game = _gameService.FindViewById(id);
            return Ok(game);
        }

        // GET: api/Game/5
        [HttpGet("GetGames/{id}", Name = "GetGames")]
        public ActionResult<string> GetGames(int id, int? stateId, int limit = 0)
        {
            List<GamesView> games = _gameService.GetGamesByUser(id, stateId, limit);
            return Ok(games);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ShortGame))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]ShortGame game)
        {
            try
            {
                return Ok(await _gameService.Create(game));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        // PUT: api/Game/5
        [HttpPut("changestate/{id}")]
        [ProducesResponseType(201, Type = typeof(ShortGame))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PutState(int id, [FromBody]byte stateId)
        {
            try
            {
                Game game = _gameService.FindById(id);
                return Ok(await _gameService.ChangeState(game, stateId));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            Game game = _gameService.FindById(id);
            if (game == null)
            {
                return NotFound();
            }

            if (await _gameService.Delete(game))
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpGet("canaccess/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CanAccess(int id, int userId)
        {
            Game game = _gameService.FindById(id);
            if (game != null && _gameService.GameIsActive(game) && _gameService.GameContainsUser(game, userId))
                return Ok(true);

            return BadRequest("Not your game !");
        }
    }
}
