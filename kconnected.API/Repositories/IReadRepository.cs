using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public interface IReadRepository<out T>
    {
        ITask<T> GetItemAsync(Guid id);
        ITask<IEnumerable<T>> GetItemsAsync();

        ITask<T> GetItemAsync( string name);
    }
}