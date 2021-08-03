using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kconnected.API.Entities
{
    public class Skill : IEntity
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = "Yet Another JS Framework";

        public List<User>? Users { get; set; }
    }
}