using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using kconnected.API.Repositories;
using kconnected.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace kconnected.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesErrorResponseType(typeof(string))]
        public async Task<ActionResult<UserDTO>> CreateUserAsync(CreateUserDTO user)
        {
            string Error = "";
            if(await _userService.ExistsAsync(user.UserName))
            {
                Error = $"{user.UserName} is not available\n";
            }
            if(await _userService.ExistsAsync(null , user.Email))
            {
                Error += $"{user.Email} is already in use\n";
            }
            if(!string.IsNullOrWhiteSpace(Error))
                return Conflict(Error);
            var createdUser = await _userService.CreateAsync(user);
            //_logger.LogInformation($"Created {createdUser} @ {DateTimeOffset.Now}");
            return CreatedAtAction(nameof(GetUserAsync), new { id = createdUser.Id }, createdUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserAsync(Guid id)
        {
            return await _userService.GetAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await _userService.GetAsync();
        }
    }
}