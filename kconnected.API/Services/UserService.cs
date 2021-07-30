using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using kconnected.API.Extensions;
using kconnected.API.Repositories;
using MorseCode.ITask;
using Microsoft.EntityFrameworkCore;

namespace kconnected.API.Services
{

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Skill> _skillRepository;

        public UserService(IRepository<User> userRepository, IRepository<Skill> skillRepository)
        {
            _userRepository = userRepository;
            _skillRepository = skillRepository;
        }
        public async Task<UserDTO> CreateAsync(CreateUserDTO item)
        {
            User toCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = item.FirstName,
                Surname = item.LastName,
                Email = item.Email,
                UserName = item.UserName,
                Skills = item.Skills.Select(x=>x.AsEntity()).ToList()
            };

            await _userRepository.AddItemAsync(toCreate);
            await _userRepository.SaveChangesAsync();

            // //create user with info
            // User toCreate = new()
            // {
            //     Id = Guid.NewGuid(),
            //     Name = item.FirstName,
            //     Surname = item.LastName,
            //     Email = item.Email,
            //     UserName = item.UserName
            // };
            // await _userRepository.AddItemAsync(toCreate);
            // _userRepository.SaveChangesAsync();
            // //with skill info create skill if not exists
            // Parallel.ForEach(item.Skills, async skill => 
            // {
            //     if (!_skillRepository.ExistsAsync(skill.Name).Result)
            //     {
            //         await _skillRepository.AddItemAsync(new Skill { Name = skill.Name, Description = skill.Description });
            //         await _skillRepository.SaveChangesAsync();
            //     }
            //      var foundSkill =  _skillRepository.GetItemAsync(skill.Name).Result;
            //     // foundSkill.Users.Add(toCreate);
            //     var foundUser =  _userRepository.GetItemAsync(toCreate.Id).Result;
            //     foundUser.Skills.Add(foundSkill);
            // });
            // await _userRepository.SaveChangesAsync();
            // await _skillRepository.SaveChangesAsync();
            // //or reference to player

            // throw new NotImplementedException();
            return _userRepository.GetItemAsync(toCreate.Id).Result.AsDTO();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            var result = _userRepository.GetItemsAsync().Result.Select(x => x.AsDTO()).ToList();
            return result;
        }

        public Task<UserDTO> UpdateAsync(UpdateUserDTO item)
        {
            throw new NotImplementedException();
        }
    }
}