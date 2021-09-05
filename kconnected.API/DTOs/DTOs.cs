using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace kconnected.API.DTOs
{




    public record CreateSkillDTO(string Name) : IWriteDTO;


    //DTOs for Read
    public record IReadDTO();
    public record UserDTO(Guid Id ,string FirstName, string LastName, string FullName,DateTimeOffset RegistrationDate, string UserName, [property : EmailAddress]  string Email, List<SkillDTO> Skills) : IReadDTO;
    public record SkillDTO(Guid Id,string Name) : IReadDTO;

    //DTOs for Update
    public record IUpdateDTO();

    public record UpdateUserDTO(string FirstName, string LastName, [param : Required]string UserName, [property : EmailAddress][param : Required] string Email) : IUpdateDTO;

    public record UpdateSkillDTO(string Name) : IUpdateDTO;

    

}