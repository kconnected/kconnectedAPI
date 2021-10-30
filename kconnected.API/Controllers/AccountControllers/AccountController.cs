using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Repositories;
using kconnected.API.Services;
using kconnected.API.Utilities;
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

        private readonly IAuthenticationManager _authManager;
        

        public AccountController(IUserService userService, IAuthenticationManager authManager)
        {
            _userService = userService;
            _authManager = authManager;
            
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
        public async Task<ActionResult> Login([Required]string email,[Required] string password)
        {
            
            var user = await _userService.GetWithEmailAsync(email);
            var token = _authManager.Authenticate(email,password);

            if(token == null)
            {
                return Unauthorized(RedirectToRoute("/api/Account/" + nameof(Register)));
            }

            return Ok(token);

        }
    }
}