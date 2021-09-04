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
using System.Collections.Concurrent;
using kconnected.API.Data;

namespace kconnected.API.Services
{

    public class UserService : IUserService
    {


        private readonly ISkillRepository _skillRepository;
        private readonly IUserRepository _userRepository;


        public UserService(ISkillRepository skillRepository, IUserRepository userRepository)
        {
            _skillRepository = skillRepository;
            _userRepository = userRepository;
        }
        public async  Task<UserDTO> CreateAsync(CreateUserDTO item)
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

            await _userRepository.AddItemAsync(toCreate);
            await _userRepository.SaveChangesAsync();

            //Add Skills here after adding the user
            await BatchAddUserSkills(toCreate.Id,item.Skills);

            return (await _userRepository.GetItemAsync(toCreate.Id)).AsDTO();
        }

        public async Task BatchAddUserSkills(Guid uid,List<CreateSkillDTO> skillList)
        {
            //var user = await _userRepository.GetItemAsync(uid);
            if(skillList.Count == 0)
            {
                throw new InvalidOperationException($"Skills list is Empty");
            }

            ConcurrentBag<Skill> skillBag = new();
            Parallel.ForEach(skillList, async skill =>{
                                
                var skillRepository = RepositoryFactory.GetRepositoryInstance<Skill,InMemorySkillRepository>(new kconnectedAPIDbContext());
                var userRepository = RepositoryFactory.GetRepositoryInstance<User,InMemoryUserRepository>(new kconnectedAPIDbContext());
                if(await skillRepository.ExistsAsync(skill.Name))
                {
                    
                    await userRepository.AddSkillAsync(uid,await skillRepository.GetItemAsync(skill.Name));
                    
                }
                else
                {
                    var newskill = skill.AsEntity();
                    await skillRepository.AddItemAsync(newskill);
                    await skillRepository.SaveChangesAsync();
                    await userRepository.AddSkillAsync(uid,await skillRepository.GetItemAsync(skill.Name));
                }
            });

            await _userRepository.SaveChangesAsync();
        }


        public Task DeleteAsync(Guid id)
        {
            return _userRepository.RemoveItemAsync(id);
        }

        public async Task<UserDTO> GetAsync(Guid id)
        {
            return (await _userRepository.GetItemAsync(id)).AsDTO();
        }

        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            return (await _userRepository.GetItemsAsync()).Select(x => x.AsDTO()).ToList();
        }

        public async Task<bool> ExistsWithUsernameAsync(string username)
        {
            
            return await _userRepository.ExistsWithUsernameAsync(username);

        }

        public async Task<bool> ExistsWithEmailAsync(string email)
        {
            
            return await _userRepository.ExistsWithEmailAsync(email);

        }

        public async Task<UserDTO> UpdateAsync(UpdateUserDTO item, Guid id)
        {
            var toUpdate = await _userRepository.GetItemAsync(id);
            toUpdate.Email = item.Email;
            toUpdate.Name = item.FirstName;
            toUpdate.Surname = item.LastName;
            await _userRepository.UpdateItemAsync(toUpdate);

            return (await _userRepository.GetItemAsync(id)).AsDTO();

        }
    }
}