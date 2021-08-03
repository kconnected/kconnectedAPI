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
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;


        public UserService(IUserRepository userRepository, ISkillRepository skillRepository)
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
                RegistrationDate = DateTimeOffset.UtcNow,
                Skills = new List<Skill>()
                
            };
            var loadingTasks = new List<Task>();
            foreach (var skill in item.Skills)
            {
                var task = UserAddSkill(toCreate,skill);
                loadingTasks.Add(task);

            }

            await Task.WhenAll(loadingTasks);  

            await _userRepository.AddItemAsync(toCreate);
            await _userRepository.SaveChangesAsync();

            return (await _userRepository.GetItemAsync(toCreate.Id)).AsDTO();
        }

        private async Task UserAddSkill(User toCreate,CreateSkillDTO skill)
        {
            if(await _skillRepository.ExistsAsync(skill.Name))
            {
                toCreate.Skills?.Add(await _skillRepository.GetItemAsync(skill.Name));
            }
            else
            {
                var newskill = skill.AsEntity();
                toCreate.Skills?.Add(newskill);
            }   
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
            return (await _userRepository.GetItemsAsync()).Select(x => x.AsDTO()).ToList();
        }

        public async Task<bool> ExistsAsync(string? username = null, string? email = null)
        {
            
            return await _userRepository.ExistsAsync(username,email);

        }

        public Task<UserDTO> UpdateAsync(UpdateUserDTO item)
        {
            throw new NotImplementedException();
        }
    }
}