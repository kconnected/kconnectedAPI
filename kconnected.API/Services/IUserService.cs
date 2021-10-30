using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;

namespace kconnected.API.Services
{
    public interface IUserService : IWriteService<CreateUserDTO, UpdateUserDTO, UserDTO>, IReadService<UserDTO>
    {
        Task<bool> ExistsWithUsernameAsync(string username);
        Task<bool> ExistsWithEmailAsync(string username);
        Task BatchAddUserSkills(Guid uid,List<CreateSkillDTO> skillList);

        Task<User> GetWithEmailAsync(string email);
        Task Follow(Guid currentUser, Guid  followed);
    }
}