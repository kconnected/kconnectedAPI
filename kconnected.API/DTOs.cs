using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using kconnected.API.Entities;

namespace kconnected.API.DTOs
{
    //DTOs for Create and Update
    public record IWriteDTO();
    public record CreateUserDTO(string FirstName, string LastName, [Required]string UserName, [EmailAddress][Required] string Email, List<CreateSkillDTO> Skills) : IWriteDTO;

    public record CreateSkillDTO(string Name, string Description) : IWriteDTO;


    //DTOs for Read
    public record IReadDTO();
    public record UserDTO(Guid Id ,string FirstName, string LastName, string FullName, string UserName,  string Email, List<SkillDTO> Skills) : IReadDTO;
    public record SkillDTO(Guid Id,string Name, string Description) : IReadDTO;

    //DTOs for Update
    public record IUpdateDTO();

    public record UpdateUserDTO(string FirstName, string LastName, [Required]string UserName, [EmailAddress][Required] string Email, List<SkillDTO> Skills) : IUpdateDTO;

}