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
                valor: orcamentoCriarRequestDto.Valor
            );
    }
    
    public static OrcamentoResponseDto ToDto(Orcamento orcamento)
    {
        return new OrcamentoResponseDto(
                Id: orcamento.Id,
                Cliente: ClienteMapper.ToDto(orcamento.Cliente),
                Descricao: orcamento.Descricao,
                Valor: orcamento.Valor
            );
    }

    public static Orcamento ToUpdateEntity(OrcamentoUpdateRequestDto orcamentoUpdateRequestDto, Orcamento orcamento)
    {
        orcamento.Atualizar(
                descricao: orcamentoUpdateRequestDto.Descricao,
                valor: orcamentoUpdateRequestDto.Valor
            );
        
        return orcamento;
    }

    public static List<OrcamentoResponseDto> ToDtoList(IEnumerable<Orcamento> listaOrcamento)
    {
        return listaOrcamento.Select(ToDto).ToList();
    }
}
