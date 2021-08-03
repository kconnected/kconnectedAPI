using kconnected.API.Entities;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        ITask<bool> ExistsAsync( string? name = null, string? email = null);
    }
}