using kconnected.API.DTOs;

namespace kconnected.API.Services
{
    public interface IUserService : IWriteService<CreateUserDTO, UpdateUserDTO, UserDTO>, IReadService<UserDTO>
    {

    }
}