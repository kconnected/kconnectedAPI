using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using kconnected.API.Repositories;
using kconnected.API.Services;
using Microsoft.AspNetCore.Authorization;
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
        

        public UserController(IUserService userService)
        {
            _userService = userService;            
        }




        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(string))]
        public async Task<ActionResult<UserDTO>> GetUserAsync(Guid id)
        {
            var toReturn = await _userService.GetAsync(id);
            if(toReturn == null)
            {
                return NotFound($"No User with Id: {id}");
            }
            
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