using Backend.Dtos.Requests;
using Backend.Enums;
using Backend.Models;

namespace Backend.Tests.Factory;

public abstract class OrcamentoMockFactoryTest
{
    public static OrcamentoCriarRequestDto ToOrcamentoCriarRequestDto()
    {
        return new OrcamentoCriarRequestDto(
            ClienteId: 1,
            Descricao: "Descrição",
            Valor: 2500
        );
    }

    public static Orcamento ToOrcamentoEntity(StatusEnum status)
    {
        return new Orcamento(
            descricao: "Descrição",
            valor: 2800,
            cliente: ClienteMockFactoryTest.ToClienteEntity(),
            status: status
        );
    }
}
