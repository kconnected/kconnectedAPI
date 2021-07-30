using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kconnected.API.Entities
{
    public class User : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }

        public string FullName { get { 
            if(string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Surname))
                return UserName;
            else
                return $"{Name} {Surname}";
         } }

        public List<Skill> Skills { get; set; }

    }
}