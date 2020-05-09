using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarGameAPI.Models;
using WarGameAPI.Services;

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        // GET: api/Game
        [HttpGet("all", Name = "GetAll")]
        public ActionResult<string> GetAll()
        {
            List<ShortShip> ships = _shipService.GetAll();
            return Ok(ships);
        }
    }
}