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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesErrorResponseType(typeof(string))]
        public async Task<ActionResult<UserDTO>> CreateUserAsync(CreateUserDTO user)
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
            //_logger.LogInformation($"Created {createdUser} @ {DateTimeOffset.Now}");
            return CreatedAtAction(nameof(GetUserAsync), new { id = createdUser.Id }, createdUser);
        }

        // [HttpPatch("{id}")]
        // public async Task<ActionResult<UserDTO>> AddUserSkills(Guid id,List<CreateSkillDTO> skillList )
        // {
        //     await _userService.BacthAddUserSkills(id,skillList);

        //     var patchedUser = _userService.GetAsync(id);

        //     return Ok(patchedUser);

        // }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<ActionResult<UserDTO>> GetUserAsync(Guid id)
        {
            var toReturn = await _userService.GetAsync(id);
            return Ok(toReturn);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await _userService.GetAsync();
        }

        
    }
}