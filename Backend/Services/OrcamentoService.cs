using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Enums;
using Backend.Exceptions;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Mappers;
using Backend.Models;
using Backend.Services.StateStatus;

namespace Backend.Services;

public class OrcamentoService : IOrcamentoService
{
    private readonly IOrcamentoRepository _repository;
    private readonly ILogger<OrcamentoService> _logger;
    private readonly IClienteService _clienteService;
    private readonly State _state;

    public OrcamentoService(IOrcamentoRepository repository, 
        ILogger<OrcamentoService> logger, 
        IClienteService clienteService,
        State state)
    {
        _repository = repository;
        _logger = logger;
        _clienteService = clienteService;
        _state = state;
    }

    public async Task<OrcamentoResponseDto> CreateAsync(OrcamentoCriarRequestDto orcamentoCriarRequestDto)
    {
        _logger.LogInformation("Iniciando - criar orçamento para o clienteId: {Id}", orcamentoCriarRequestDto.ClienteId);
        
        var cliente = await _clienteService.GetById(orcamentoCriarRequestDto.ClienteId);
        var orcamento = OrcamentoMapper.ToEntity(orcamentoCriarRequestDto, cliente);
        await _repository.Save(orcamento);
        
        _logger.LogInformation("Encerrado - criar orçamento para o clienteId: {Id}", orcamentoCriarRequestDto.ClienteId);
        
        return OrcamentoMapper.ToDto(orcamento);
    }


    public async Task<OrcamentoResponseDto> UpdateAsync(int id, OrcamentoUpdateRequestDto orcamentoUpdateRequestDto)
    {
        _logger.LogInformation("Iniciando - atualizar por orçamento de id: {Id}", id);
        
        var orcamento = await GetById(id);
        await _repository.Update(OrcamentoMapper.ToUpdateEntity(orcamentoUpdateRequestDto, orcamento, orcamento.Status));
        
        _logger.LogInformation("Encerrado - atualizar por orçamento de id: {Id}", id);
        
        return OrcamentoMapper.ToDto(orcamento);
    }

    private async Task<OrcamentoResponseDto> MudarStatus(int id,
        StatusEnum concluido)
    {
        _logger.LogInformation("Iniciando - avançar o status do orçamento de id: {Id}", id);
        
        var orcamento = await GetById(id);
        var proximoStatus = _state.ProcessarMudancaStatus(orcamento, concluido);
        await _repository.Update(OrcamentoMapper.ToUpdateStatusEntity(orcamento, proximoStatus));
        
        _logger.LogInformation("Encerrado - avançar o status do orçamento de id: {Id}", id);
        
        return OrcamentoMapper.ToDto(orcamento);
    }

    public async Task<OrcamentoResponseDto> ConcluirOrcamento(int id)
    {
        return await MudarStatus(id, StatusEnum.Concluido);
    }

    public async Task<OrcamentoResponseDto> CancelarOrcamento(int id)
    {
        return await MudarStatus(id, StatusEnum.Cancelado);
    }
    
    public async Task<OrcamentoResponseDto> AvaliarOrcamento(int id)
    {
        return await MudarStatus(id, StatusEnum.Avaliando);
    }

    public async Task<OrcamentoResponseDto> FindByIdAsync(int id)
    {
        _logger.LogInformation("Iniciando - buscar por orçamento de id: {Id}", id);
        
        var orcamento = await GetById(id); 
        
        _logger.LogInformation("Encerrado - buscar por orçamento de id: {Id}", id);
        
        return OrcamentoMapper.ToDto(orcamento);
    }

    public async Task<List<OrcamentoResponseDto>> FindAllAsync()
    {
        _logger.LogInformation("Iniciando - buscar lista de orçamentos");

        var listaOrcamento = await _repository.FindAll(); 
        
        _logger.LogInformation("Encerrado - buscar lista de orçamentos");
        
        return OrcamentoMapper.ToDtoList(listaOrcamento);
    }

    private async Task<Orcamento> GetById(int id)
    {
        _logger.LogInformation("Iniciando - consultar orçamento com o id: {Id}", id);
        var orcamento = await _repository.FindById(id);

        if (orcamento == null)
        {
            _logger.LogInformation("Encerrado - consultar orçamento com o id: {Id}. Erro: {Erro}", id, typeof(NotFoundException));
            throw new NotFoundException($"Orçamento com id: {id} não foi encontrado");
        }
        
        _logger.LogInformation("Encerrado - consultar orçamento com o id: {Id}", id);
        
        return orcamento;
    }
}
