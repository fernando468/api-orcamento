using Backend.Dtos.Requests;
using Backend.Dtos.Responses;

namespace Backend.Interfaces.Services;

public interface IOrcamentoService
{
    Task<OrcamentoResponseDto> CreateAsync(OrcamentoCriarRequestDto orcamentoCriarRequestDto);
    Task<OrcamentoResponseDto> UpdateAsync(int id, OrcamentoUpdateRequestDto orcamentoUpdateRequestDto);
    Task<OrcamentoResponseDto> FindByIdAsync(int id);
    Task<List<OrcamentoResponseDto>> FindAllAsync();
    Task<OrcamentoResponseDto> ConcluirOrcamento(int id);
    Task<OrcamentoResponseDto> CancelarOrcamento(int id);
    Task<OrcamentoResponseDto> AvaliarOrcamento(int id);
}
