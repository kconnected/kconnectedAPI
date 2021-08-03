using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.Entities;
using Microsoft.EntityFrameworkCore;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<User>();
        }
        public async Task AddItemAsync(User entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async ITask<User> GetItemAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ITask<IEnumerable<User>> GetItemsAsync()
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

        public async Task UpdateItemAsync(User entity)
        {
            var item = await _dbSet.FindAsync(entity.Id);
            _dbSet.Update(item);
        }

        public async ITask<bool> ExistsAsync( string? name = null, string? email = null)
        {
            return await _dbSet.AnyAsync(e => e.Name == name  || e.Email == email );
        }

        public async ITask<User> GetItemAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}