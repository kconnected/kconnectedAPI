using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kconnected.API.DTOs;

namespace kconnected.API.Services
{
    public interface IReadService<TResulDTO>
    where TResulDTO : IReadDTO
    {
         
        Task<TResulDTO> GetAsync(Guid id);

        Task<IEnumerable<TResulDTO>> GetAsync();
         
    }


}