using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;

namespace kconnected.API.Services
{
    public interface IUserService : IWriteService<CreateUserDTO, UpdateUserDTO, UserDTO>, IReadService<UserDTO>
    {
        Task<bool> ExistsWithUsernameAsync(string username);
        Task<bool> ExistsWithEmailAsync(string username);
        Task BatchAddUserSkills(Guid uid,List<CreateSkillDTO> skillList);
    }
}