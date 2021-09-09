using System;
using System.Threading.Tasks;
using kconnected.API.Entities;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        ITask<bool> ExistsWithEmailAsync(string email);
        ITask<bool> ExistsWithUsernameAsync(string name);

        ITask<User> AddSkillAsync(Guid id, Skill skill);
        Task<User> GetWithEmailAsync(string email);
    }
}