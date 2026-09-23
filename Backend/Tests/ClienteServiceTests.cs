using Backend.Dtos.Requests;
using Backend.Exceptions;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Mappers;
using Backend.Models;
using Backend.Services;
using Backend.Tests.Factory;
using Moq;
using Xunit;

namespace Backend.Tests;

public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly ClienteService _service;

    public ClienteServiceTests()
    {
        var logger = new LoggerFactory().CreateLogger<ClienteService>();
        
        _repositoryMock = new Mock<IClienteRepository>();
        _service = new ClienteService(_repositoryMock.Object, logger);
    }

    [Fact]
    public async Task DeveCriarNovoCliente()
    {
        var clienteRequestDto = ClienteMockFactoryTest.ToClienteResponseDto();
        var clienteCriado = await _service.CreateAsync(clienteRequestDto);
        
        Assert.NotNull(clienteCriado);
        Assert.Equal("Nome", clienteCriado.Nome);
        Assert.Equal("12345678910", clienteCriado.Cpf);
        Assert.Equal("email@email.com", clienteCriado.Email);
        Assert.Equal("44900001111", clienteCriado.Telefone);
    }

    [Fact]
    public async Task DeveOcorrerErroQuandoClienteComMesmoCpf()
    {
        var clienteRequestDto = ClienteMockFactoryTest.ToClienteResponseDto();
        var clienteExistente = ClienteMapper.ToEntity(clienteRequestDto);
        _repositoryMock
            .Setup(repository => repository.FindByCpfAsync(clienteRequestDto.Cpf))
            .ReturnsAsync(clienteExistente);

        var excecao = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.CreateAsync(clienteRequestDto)
        );

        Assert.Equal("Cliente com este CPF já cadastrado", excecao.Message);
    
        _repositoryMock.Verify(r => r.Save(It.IsAny<Cliente>()), Times.Never);
    }
    
    [Fact]
    public async Task DeveOcorrerErroQuandoClienteComMesmoEmail()
    {
        var clienteRequestDto = ClienteMockFactoryTest.ToClienteResponseDto();
        var clienteExistente = ClienteMapper.ToEntity(clienteRequestDto);
        _repositoryMock
            .Setup(repository => repository.FindByEmailAsync(clienteRequestDto.Email))
            .ReturnsAsync(clienteExistente);

        var excecao = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.CreateAsync(clienteRequestDto)
        );

        Assert.Equal("Cliente com este E-mail já cadastrado", excecao.Message);
    
        _repositoryMock.Verify(r => r.Save(It.IsAny<Cliente>()), Times.Never);
    }

    [Fact]
    public async Task DeveBuscarComSucessoClientePorId()
    {
        var cliente = ClienteMockFactoryTest.ToClienteEntity();
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(cliente);
        
        PropertyReflection<Cliente>.Set(field: "Id", type: cliente, value: 1);
        
        var clienteResponseDto = await _service.FindByIdAsync(1);
        
        Assert.NotNull(clienteResponseDto);
        Assert.Equal(1, clienteResponseDto.Id);
        Assert.Equal("Nome", clienteResponseDto.Nome);
        Assert.Equal("12345678910", clienteResponseDto.Cpf);
        Assert.Equal("email@email.com", clienteResponseDto.Email);
        Assert.Equal("44900001111", clienteResponseDto.Telefone);
    }
    
    [Fact]
    public async Task DeveRetornarErroQuandoClienteNaoExistePorId()
    {
        var excecao = await Assert.ThrowsAsync<NotFoundException>(
            () => _service.FindByIdAsync(1)
        );

        Assert.Equal("Cliente com id: 1 não encontrado", excecao.Message);
    
        _repositoryMock.Verify(r => r.Save(It.IsAny<Cliente>()), Times.Never);
    }
}
