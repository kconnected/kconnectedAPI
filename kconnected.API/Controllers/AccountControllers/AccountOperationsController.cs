using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Services;
using Microsoft.AspNetCore.Mvc;
//using Controllers.Models;

namespace kconnected.API.Controllers
{
    [Route("api/Account/Operations/[Action]")]
    [ApiController]
    public class AccountOperationsController : ControllerBase
    {
        private readonly IUserService _userService; 
        public AccountOperationsController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserFollowedListAsync()
        {
            var currentUser = await _userService.GetAsync(new Guid(HttpContext?.User.FindFirst("Id").Value));

            return Ok(currentUser.FollowedUsers);
        }


        [HttpPost]
        public async Task<ActionResult> FollowAsync(Guid followedId)
        {
            var currentUser = await _userService.GetAsync(new Guid(HttpContext?.User.FindFirst("Id").Value));
            await _userService.Follow(currentUser.Id , followedId);

            return CreatedAtAction(nameof(GetUserFollowedListAsync),currentUser.Id);

        }
    }
}