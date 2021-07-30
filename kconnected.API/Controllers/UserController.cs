using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using kconnected.API.Repositories;
using kconnected.API.Services;
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
        public async Task<ActionResult<UserDTO>> CreateUserAsync(CreateUserDTO user)
        {
            
            var createdUser = await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(GetUserAsync), new { id = createdUser.Id }, createdUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await _userService.GetAsync();
        }
    }
}