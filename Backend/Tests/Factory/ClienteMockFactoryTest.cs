using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;

namespace Backend.Tests.Factory;

public abstract class ClienteMockFactoryTest
{
    public static ClienteRequestDto ToClienteResponseDto()
    {
        return new ClienteRequestDto(
            Nome: "Nome",
            Cpf: "12345678910",
            Telefone: "44900001111",
            Email: "email@email.com"
        );
    }

    public static Cliente ToClienteEntity()
    {
        return new Cliente(
            nome: "Nome",
            cpf: "12345678910",
            telefone: "44900001111",
            email: "email@email.com"
        );
    }
}
