using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kconnected.API.Data;
using kconnected.API.DTOs;
using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        protected readonly kconnectedAPIDbContext _dbContext;

        public InMemoryUserRepository(kconnectedAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async  Task AddItemAsync(User entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async ITask<User> GetItemAsync(Guid id)
        {
            //onyl await if you need the result instantly so wihtout awaiting this call 
            //returning from this method does not make sense awaiting looks like best practice here
            return await _dbContext.Users.Include(s => s.Skills).FirstOrDefaultAsync(e => e.Id == id);
            
        }

        public async ITask<User> AddSkillAsync(Guid id, Skill skill)
        {
            var user = await _dbContext.Users.Include(t => t.Skills).FirstOrDefaultAsync(u => u.Id == id);
            if(user?.Skills?.Any(s => s.Name == skill.Name) ?? false)
            {
                throw new InvalidOperationException($" User {user.Id}:{user.UserName} already knows {skill.Name}");
            }
            user?.Skills?.Add(skill);

            await SaveChangesAsync();
            return await _dbContext.Users.Include(t => t.Skills).FirstOrDefaultAsync(u => u.Id == id);

        }

        public async ITask<IEnumerable<User>> GetItemsAsync()
        {
            return await _dbContext.Users.Include( t => t.Skills).ToListAsync();
            
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var item = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateItemAsync(User entity)
        {
            return Task.FromResult(_dbContext.Update(entity));
            
        }

        public async ITask<bool> ExistsWithEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == email);
        }

        public async ITask<bool> ExistsWithUsernameAsync(string name)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == name);
        }

        public async Task<User> GetWithEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }


        public async ITask<User> GetItemAsync(string username)
        {
            return await _dbContext.Users.Include(s => s.Skills).FirstOrDefaultAsync(e => e.UserName == username);
        }
    }
}