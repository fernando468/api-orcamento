using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Models;

namespace Backend.Mappers;

public abstract class OrcamentoMapper
{
    public static Orcamento ToEntity(OrcamentoCriarRequestDto orcamentoCriarRequestDto, Cliente cliente)
    {
        return new Orcamento(
                descricao: orcamentoCriarRequestDto.Descricao,
                cliente: cliente,
                valor: orcamentoCriarRequestDto.Valor,
                status: StatusEnum.AguardandoAvaliacao
            );
    }
    
    public static OrcamentoResponseDto ToDto(Orcamento orcamento)
    {
        return new OrcamentoResponseDto(
                Id: orcamento.Id,
                Cliente: ClienteMapper.ToDto(orcamento.Cliente),
                Descricao: orcamento.Descricao,
                Valor: orcamento.Valor,
                Status: orcamento.Status
            );
    }

    public static Orcamento ToUpdateEntity(OrcamentoUpdateRequestDto orcamentoUpdateRequestDto, Orcamento orcamento, StatusEnum status)
    {
        orcamento.Atualizar(
                descricao: orcamentoUpdateRequestDto.Descricao,
                valor: orcamentoUpdateRequestDto.Valor,
                status: status
            );
        
        return orcamento;
    }

    public static Orcamento ToUpdateStatusEntity(Orcamento orcamento, StatusEnum status)
    {
        orcamento.AtualizarStatus(status);
        return orcamento;
    }

    public static List<OrcamentoResponseDto> ToDtoList(IEnumerable<Orcamento> listaOrcamento)
    {
        return listaOrcamento.Select(ToDto).ToList();
    }
}
