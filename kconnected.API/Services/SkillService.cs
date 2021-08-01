using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using kconnected.API.Extensions;
using kconnected.API.Repositories;

namespace kconnected.API.Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> _skillRepository;

        public SkillService(IRepository<Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public Task<SkillDTO> CreateAsync(CreateSkillDTO item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<SkillDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SkillDTO>> GetAsync()
        {
           return _skillRepository.GetItemsAsync().Result.Select(x => x.AsDTO()).ToList();
        }

        public Task<SkillDTO> UpdateAsync(UpdateSkillDTO item)
        {
            throw new NotImplementedException();
        }
    }
}