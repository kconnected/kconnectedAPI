using System;

namespace kconnected.API.Entities
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}