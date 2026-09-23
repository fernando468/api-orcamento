using Backend.Dtos.Requests;
using Backend.Dtos.Responses;
using Backend.Exceptions;
using Backend.Interfaces;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Mappers;
using Backend.Models;

namespace Backend.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly ILogger<ClienteService> _logger;

    public ClienteService(IClienteRepository iClienteRepository, ILogger<ClienteService> logger)
    {
        _repository = iClienteRepository;
        _logger = logger;
    }

    public async Task<ClienteResponseDto> CreateAsync(ClienteRequestDto clienteRequestDto)
    {
        _logger.LogInformation("Iniciando - criar cliente: {Cpf}", clienteRequestDto.Cpf.Substring(0, 3));
        
        var clientePorCpf = await _repository.FindByCpfAsync(clienteRequestDto.Cpf);
        if (clientePorCpf != null)
        {
            throw new InvalidOperationException("Cliente com este CPF já cadastrado");
        }

        var clientePorEmail = await _repository.FindByEmailAsync(clienteRequestDto.Email);
        
        if (clientePorEmail != null)
        {
            throw new InvalidOperationException("Cliente com este E-mail já cadastrado");
        }

        var cliente = ClienteMapper.ToEntity(clienteRequestDto);
        await _repository.Save(cliente);
        
        _logger.LogInformation("Encerrado - criar cliente: {Cpf}", clienteRequestDto.Cpf.Substring(0, 3));
        
        return ClienteMapper.ToDto(cliente);
    }

    public async Task<ClienteResponseDto> UpdateAsync(int id, ClienteRequestDto clienteRequestDto)
    {
        _logger.LogInformation("Iniciando - atualizar cliente com o id: {Id}", id);
        
        var cliente = await GetById(id);
        await _repository.Update(ClienteMapper.ToUpdateEntity(clienteRequestDto, cliente));
        
        _logger.LogInformation("Encerrado - atualizar cliente com o id: {Id}", id);
        
        return ClienteMapper.ToDto(cliente);
    }

    public async Task<ClienteResponseDto?> FindByIdAsync(int id)
    {
        _logger.LogInformation("Iniciando - buscar cliente com o id: {Id}", id);
        
        var cliente = await GetById(id);
        
        _logger.LogInformation("Encerrado - buscar cliente com o id: {Id}", id);
        
        return ClienteMapper.ToDto(cliente);
    }

    public async Task<Cliente> GetById(int id)
    {
        _logger.LogInformation("Iniciando - consultar cliente com o id: {Id}", id);
        
        var cliente = await _repository.FindById(id);

        if (cliente == null)
        {
            _logger.LogError("Encerrado - consultar cliente com o id: {Id}. Erro: {Erro}", id, typeof(NotFoundException));
            throw new NotFoundException($"Cliente com id: {id} não encontrado");
        }

        _logger.LogInformation("Encerrado - consultar cliente com o id: {Id}", id);
        
        return cliente;
    }

    public async Task<List<ClienteResponseDto>> FindAllAsync()
    {
        _logger.LogInformation("Iniciando - buscar lista com todos os clientes");
        
        var listaCliente = await _repository.FindAll();
        
        _logger.LogInformation("Encerrado - buscar lista com todos os clientes");
        
        return ClienteMapper.ToListDto(listaCliente);
    }

    public async Task DeleteById(int id)
    {
        _logger.LogInformation("Iniciando - excluir cliente com o id: {Id}", id);
        
        var cliente = await GetById(id);

        await _repository.Delete(cliente);
        
        _logger.LogInformation("Encerrado - excluir cliente com o id: {Id}", id);
    }
}
