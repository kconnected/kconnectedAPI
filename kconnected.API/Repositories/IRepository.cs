using System;
using System.Threading.Tasks;
using kconnected.API.Entities;

namespace kconnected.API.Repositories
{
    public interface IRepository<T> : IWriteRepository<T>, IReadRepository<T>
    where T : IEntity
    {
        public Task SaveChangesAsync();
    }
}