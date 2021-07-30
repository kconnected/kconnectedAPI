using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;
using kconnected.API.Entities;

namespace kconnected.API.Services
{
    public interface IWriteService<TCreateDTO, TUpdateDTO, TResultDTO>
     where TCreateDTO : IWriteDTO
     where TUpdateDTO : IUpdateDTO
     where TResultDTO : IReadDTO
    {        
        Task<TResultDTO> CreateAsync(TCreateDTO item);
        Task<TResultDTO> UpdateAsync(TUpdateDTO item);
        Task DeleteAsync(Guid id);
    }
    public interface IReadService<TResulDTO>
    where TResulDTO : IReadDTO
    {
         
        Task<TResulDTO> GetAsync(Guid id);

        Task<IEnumerable<TResulDTO>> GetAsync();
         
    }


}