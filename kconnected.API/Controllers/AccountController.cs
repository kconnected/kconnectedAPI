using System;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace kconnected.API.Controllers
{
    [ApiController]
    [Route("api/Account/[Action]")]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        

        public AccountController(IUserService userService)
        {
            _userService = userService;
            
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesErrorResponseType(typeof(string))]
        public async Task<ActionResult<UserDTO>> Register(CreateUserDTO user)
        {
            string Error = "";
            if(await _userService.ExistsWithUsernameAsync(user.UserName))
            {
                Error = $"Username {user.UserName} is not available\n";
            }
            if(await _userService.ExistsWithEmailAsync(user.Email))
            {
                Error += $"Email {user.Email} is already in use\n";
            }
            if(!string.IsNullOrWhiteSpace(Error))
                return Conflict(Error);
            var createdUser = await _userService.CreateAsync(user);

            Log.Information($"Created {createdUser} @ {DateTimeOffset.Now}");
            
            return CreatedAtAction(nameof(UserController.GetUserAsync),"User", new { id = createdUser.Id }, createdUser);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(string Username)
        {
            
            return Ok();

        }
    }
}