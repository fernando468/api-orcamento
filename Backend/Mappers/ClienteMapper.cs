using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;

namespace Backend.Mappers;

public abstract class ClienteMapper
{
    public static Cliente ToEntity(ClienteRequestDto clienteRequestDto)
    {
        return new Cliente(
            nome: clienteRequestDto.Nome,
            cpf: clienteRequestDto.Cpf,
            email: clienteRequestDto.Email,
            telefone: clienteRequestDto.Telefone
        );
    }

    public static Cliente ToUpdateEntity(ClienteRequestDto clienteRequestDto, Cliente cliente)
    {
        cliente.Atualizar(
            nome: clienteRequestDto.Nome,
            cpf: clienteRequestDto.Cpf,
            email: clienteRequestDto.Email,
            telefone: clienteRequestDto.Telefone
        );
        
        return cliente;
    }

    public static ClienteResponseDto ToDto(Cliente cliente)
    {
        return new ClienteResponseDto(
            Id: cliente.Id,
            Nome: cliente.Nome,
            Cpf: cliente.Cpf,
            Email: cliente.Email,
            Telefone: cliente.Telefone
        );
    }

    public static List<ClienteResponseDto> ToListDto(IEnumerable<Cliente> listaCliente)
    {
        return listaCliente.Select(ToDto).ToList();
    }
    
}
