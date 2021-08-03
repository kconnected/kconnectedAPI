using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kconnected.API.DTOs
{
    //DTOs for Create and Update
    public record IWriteDTO();
    public record CreateUserDTO(string FirstName, string LastName, [property : Required] string UserName, [property : EmailAddress][property : Required] string Email, List<CreateSkillDTO> Skills) : IWriteDTO;
    public record CreateSkillDTO(string Name) : IWriteDTO;


    //DTOs for Read
    public record IReadDTO();
    public record UserDTO(Guid Id ,string FirstName, string LastName, string FullName,DateTimeOffset RegistrationDate, string UserName,  string Email, List<SkillDTO> Skills) : IReadDTO;
    public record SkillDTO(Guid Id,string Name) : IReadDTO;

    //DTOs for Update
    public record IUpdateDTO();

    public record UpdateUserDTO(string FirstName, string LastName, [property : Required]string UserName, [property : EmailAddress][property : Required] string Email, List<SkillDTO> Skills) : IUpdateDTO;

    public record UpdateSkillDTO(string Name) : IUpdateDTO;

}