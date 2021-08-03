using System.Threading.Tasks;
using kconnected.API.DTOs;

namespace kconnected.API.Services
{
    public interface IUserService : IWriteService<CreateUserDTO, UpdateUserDTO, UserDTO>, IReadService<UserDTO>
    {
        Task<bool> ExistsAsync(string? username = null, string? email = null);
    }
}