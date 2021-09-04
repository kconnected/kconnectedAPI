using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.Data;
using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public class InMemorySkillRepository : ISkillRepository
    {
        protected readonly kconnectedAPIDbContext _dbContext;

        public InMemorySkillRepository(kconnectedAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddItemAsync(Skill entity)
        {
            await _dbContext.Skills.AddAsync(entity);
        }

        public async ITask<Skill> GetItemAsync(Guid id)
        {
            return await _dbContext.Skills.FindAsync(id);
        }

        public async ITask<IEnumerable<Skill>> GetItemsAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var item = await _dbContext.Skills.FindAsync(id);
            _dbContext.Skills.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Skill entity)
        {
            var item = await _dbContext.Skills.FindAsync(entity.Id);
            _dbContext.Skills.Update(item);
        }

        public async ITask<bool> ExistsAsync( string name )
        {
            return await _dbContext.Skills.AnyAsync(e => e.Name == name  );
        }

        public async ITask<Skill> GetItemAsync(string name)
        {
            return await _dbContext.Skills.FirstOrDefaultAsync(e => e.Name == name);
        }

        
        
    }
}