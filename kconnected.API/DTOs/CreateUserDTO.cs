using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kconnected.API.DTOs
{
    //public record CreateUserDTO(string FirstName, string LastName, [param : Required] string UserName, [param : EmailAddress][param : Required] string Email, List<CreateSkillDTO> Skills) : IWriteDTO;

#nullable disable
    public record CreateUserDTO : IWriteDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string UserName{get;set;}

        [EmailAddress]
        [Required]
        public string Email{get;set;}

        [Required]
        public string Password{get;set;}

        public List<CreateSkillDTO> Skills {get; set;}

    }

}