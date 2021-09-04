using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kconnected.API.Entities
{
    public class Skill : IEntity
    {

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = "Yet Another JS Framework";

        public virtual List<User>? Users { get; set; }
    }
}