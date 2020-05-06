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
    public class FriendController : ControllerBase
    {
        private IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            List<FriendsView> friends = _friendService.FindViewsByUserId(id);
            if (friends == null)
            {
                return NotFound();
            }

            return Ok(friends);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ShortFriend))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]ShortFriend friend)
        {
            try
            {
                return Ok(await _friendService.Create(friend));
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(201, Type = typeof(ShortFriend))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody]ShortFriend shortFriend)
        {
            
            Friends friend = _friendService.FindByAssociation(shortFriend);
            if (friend == null)
            {
                return NotFound();
            }

            try
            {
                FriendsView friendsView = await _friendService.Update(friend);
                return Ok(friendsView);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.Message);
                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete([FromBody]ShortFriend shortFriend)
        {
            Friends friend = _friendService.FindByAssociation(shortFriend);
            if (friend == null)
            {
                return NotFound();
            }

            if (await _friendService.Delete(friend))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}