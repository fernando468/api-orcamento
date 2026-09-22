using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Exceptions;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Mappers;
using Backend.Models;

namespace Backend.Services;

public class OrcamentoService : IOrcamentoService
{
    private readonly IOrcamentoRepository _repository;
    private readonly ILogger<OrcamentoService> _logger;
    private readonly IClienteService _clienteService;

    public OrcamentoService(IOrcamentoRepository repository, 
        ILogger<OrcamentoService> logger, 
        IClienteService clienteService)
    {
        _repository = repository;
        _logger = logger;
        _clienteService = clienteService;
    }


    public async Task<OrcamentoResponseDto> CreateAsync(OrcamentoCriarRequestDto orcamentoCriarRequestDto)
    {
        _logger.LogInformation("Iniciando - criar orçamento para o clienteId: {Id}", orcamentoCriarRequestDto.ClienteId);
        
        var cliente = await _clienteService.GetById(orcamentoCriarRequestDto.ClienteId);
        var orcamento = OrcamentoMapper.ToEntity(orcamentoCriarRequestDto, cliente);
        
        _logger.LogInformation("Encerrado - criar orçamento para o clienteId: {Id}", orcamentoCriarRequestDto.ClienteId);
        
        return OrcamentoMapper.ToDto(orcamento);
    }
    

    public async Task<OrcamentoResponseDto> UpdateAsync(int id, OrcamentoUpdateRequestDto orcamentoUpdateRequestDto)
    {
        _logger.LogInformation("Iniciando - atualizar por orçamento de id: {Id}", id);
        
        var orcamento = await GetById(id);
        await _repository.Update(OrcamentoMapper.ToUpdateEntity(orcamentoUpdateRequestDto, orcamento));
        
        _logger.LogInformation("Encerrado - atualizar por orçamento de id: {Id}", id);
        
        return OrcamentoMapper.ToDto(orcamento);
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

    protected async Task<Orcamento> GetById(int id)
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
