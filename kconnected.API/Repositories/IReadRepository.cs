using System;
using System.Collections.Generic;
using MorseCode.ITask;

namespace kconnected.API.Repositories
{
    public interface IReadRepository<out T>
    {
        ITask<T> GetItemAsync(Guid id);
        ITask<IEnumerable<T>> GetItemsAsync();

        ITask<bool> ExistsAsync( string name );

        ITask<T> GetItemAsync( string name);
    }
}