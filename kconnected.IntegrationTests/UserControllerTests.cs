// using System;
// using Xunit;
// using System.Net.Http;
// using System.Net;
// using kconnected.API;
// using kconnected.API.DTOs;
// using Microsoft.AspNetCore.Mvc.Testing;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using System.Net.Http.Json;
// using FluentAssertions;


// namespace kconnected.IntegrationTests
// {
//     public class UserControllerTests : IntegrationTests
//     {
//         protected readonly string subURI = "/User";
//         public CreateUserDTO GenerateValidUser()
//         {
//             return new CreateUserDTO(
//                 FirstName : Guid.NewGuid().ToString(),
//                 LastName : Guid.NewGuid().ToString(),
//                 Email : Guid.NewGuid().ToString() + "@gmail.com",
//                 UserName : Guid.NewGuid().ToString(),
//                 Skills : GenerateSkillSet()
//             );
//         }

//         public  List<CreateSkillDTO> GenerateSkillSet()
//         {
//             return new List<CreateSkillDTO>()
//             {
//                 new CreateSkillDTO(Guid.NewGuid().ToString()),
//                 new CreateSkillDTO(Guid.NewGuid().ToString()),
//                 new CreateSkillDTO(Guid.NewGuid().ToString()),
//                 new CreateSkillDTO(Guid.NewGuid().ToString()),
//                 new CreateSkillDTO(Guid.NewGuid().ToString()),
//                 new CreateSkillDTO(Guid.NewGuid().ToString())
//             };
//         }


//         [Fact]
//         public async Task CreateUserAsync_ValidUser_ReturnsUser()
//         {
//             // Arrange
//             CreateUserDTO user = GenerateValidUser();
//             var response = await _client.PostAsJsonAsync(_URI + subURI,user);

//             response.StatusCode.Should().Be(HttpStatusCode.Created);
            
            
//         }
//     }
// }