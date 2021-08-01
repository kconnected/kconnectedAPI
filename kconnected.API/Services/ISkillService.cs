using kconnected.API.DTOs;

namespace kconnected.API.Services
{
    public interface ISkillService : IWriteService<CreateSkillDTO, UpdateSkillDTO, SkillDTO>, IReadService<SkillDTO>
    {
        
    }
}