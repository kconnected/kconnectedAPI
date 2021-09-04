using System;
using kconnected.API.Entities;

namespace kconnected.API.Repositories
{

    public class RepositoryFactory 
    {

        public static TRepository GetRepositoryInstance<T, TRepository>(params object[] args) 
        where TRepository : IRepository<T>
        where T : IEntity {
            return (TRepository)Activator.CreateInstance(typeof(TRepository), args);
        }
    }
}