using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarGameAPI.Entities;
using WarGameAPI.Models;
using WarGameAPI.Services;

namespace WarGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Nickname, model.Password);

            if (user == null)
                return BadRequest(new { message = "Nickname or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("verifytoken")]
        public IActionResult VerifyToken([FromBody]Token token)
        {
            if (_userService.VerifyToken(token.TokenString))
            {
                return Ok(true);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(201, Type = typeof(ShortUser))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]ShortUser user)
        {
            try
            {
                return Ok(await _userService.Create(user));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}