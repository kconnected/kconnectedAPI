using kconnected.API.Entities;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        ITask<bool> ExistsAsync( string name );
    }
}