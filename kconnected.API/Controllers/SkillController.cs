using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace kconnected.API.Controllers
{
    [ApiController]
    [Route("api/Skill")]
    public class SkillController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IEnumerable<SkillDTO>> GetSkillAsync()
        {
            return await _skillService.GetAsync();
        }


    }
}