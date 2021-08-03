using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<Skill> _dbSet;

        public SkillRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Skill>();
        }
        public async Task AddItemAsync(Skill entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async ITask<Skill> GetItemAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ITask<IEnumerable<Skill>> GetItemsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task RemoveItemAsync(Guid id)
        {
            var item = await _dbSet.FindAsync(id);
            _dbSet.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Skill entity)
        {
            var item = await _dbSet.FindAsync(entity.Id);
            _dbSet.Update(item);
        }

        public async ITask<bool> ExistsAsync( string name )
        {
            return await _dbSet.AnyAsync(e => e.Name == name  );
        }

        public async ITask<Skill> GetItemAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}