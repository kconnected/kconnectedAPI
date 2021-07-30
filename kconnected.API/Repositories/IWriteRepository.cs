using System;
using System.Threading.Tasks;

namespace kconnected.API.Repositories
{
    public interface IWriteRepository<in T>
    {
        Task AddItemAsync(T entity);
        Task UpdateItemAsync(T entity);
        Task RemoveItemAsync(Guid id);
    }
}