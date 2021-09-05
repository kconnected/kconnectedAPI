using System;
using System.Collections.Generic;
using System.Linq;
using kconnected.API.DTOs;
using kconnected.API.Entities;

namespace kconnected.API.Extensions
{
    public static class DTOExtensions
    {
        public static UserDTO AsDTO(this User user)
        {
            return new UserDTO(Id: user.Id, FirstName: user.Name,LastName: user.Surname, FullName: user.FullName,RegistrationDate: user.RegistrationDate, UserName: user.UserName ,Email: user.Email, Skills: user.Skills?.Select(x => x.AsDTO()).ToList() ?? new List<SkillDTO>());
        }

        public static SkillDTO AsDTO(this Skill skill)
        {
            return new SkillDTO(Id: skill.Id, Name: skill.Name);

        }

        public static Skill AsEntity(this CreateSkillDTO skillDTO)
        {
            return new Skill{Id = Guid.NewGuid(), Name = skillDTO.Name};
        }
        
        
    }
}