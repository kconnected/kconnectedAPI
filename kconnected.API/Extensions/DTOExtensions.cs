using System;
using System.Linq;
using kconnected.API.DTOs;
using kconnected.API.Entities;

namespace kconnected.API.Extensions
{
    public static class DTOExtensions
    {
        public static UserDTO AsDTO(this User user)
        {
            return new UserDTO(Id: user.Id, FirstName: user.Name,LastName: user.Surname, FullName: user.FullName, UserName: user.UserName ,Email: user.Email, Skills: user.Skills.Select(x => x.AsDTO()).ToList() );
        }

        public static SkillDTO AsDTO(this Skill skill)
        {
            return new SkillDTO(Id: skill.Id, Name: skill.Name, Description: skill.Description);

        }

        public static Skill AsEntity(this CreateSkillDTO skillDTO)
        {
            return new Skill{Id = Guid.NewGuid(), Name = skillDTO.Name, Description = skillDTO.Description};
        }
        
        
    }
}