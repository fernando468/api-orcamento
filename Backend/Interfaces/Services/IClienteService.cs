using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;

namespace Backend.Interfaces.Services;

public interface IClienteService
{
    Task<ClienteResponseDto> CreateAsync(ClienteRequestDto clienteRequestDto);
    Task<ClienteResponseDto> UpdateAsync(int id, ClienteRequestDto clienteRequestDto);
    Task<ClienteResponseDto> FindByIdAsync(int id);
    Task<Cliente> GetById(int id);
    Task<List<ClienteResponseDto>> FindAllAsync();
    Task DeleteById(int id);
}
