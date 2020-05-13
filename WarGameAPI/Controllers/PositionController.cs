using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarGameAPI.Entities;
using WarGameAPI.Models;
using WarGameAPI.Services;

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet("getbyuserandgame", Name = "GetByUserAndGame")]
        public ActionResult<string> GetByUserAndGame(int gameId, int userId)
        {
            List<ShortPosition> positions = _positionService.GetAllByUserAndGame(userId, gameId);
            return Ok(positions);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]List<Position> positions)
        {
            try
            {
                return Ok(await _positionService.Create(positions));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}