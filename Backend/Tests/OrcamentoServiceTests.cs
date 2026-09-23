using Backend.Dtos.Requests;
using Backend.Enums;
using Backend.Exceptions;
using Backend.Interfaces.Repostiories;
using Backend.Interfaces.Services;
using Backend.Models;
using Backend.Services;
using Backend.Services.StateStatus;
using Backend.Tests.Factory;
using Moq;
using Xunit;

namespace Backend.Tests;

public class OrcamentoServiceTests
{
    private readonly Mock<IOrcamentoRepository> _repositoryMock;
    private readonly OrcamentoService _service;
    private readonly ILogger<OrcamentoService> _logger;
    private readonly Mock<IClienteService> _clienteServiceMock;
    private readonly State _state;
    
    private readonly AguardandoAvaliacaoState _aguardandoAvaliacaoState = new();
    private readonly AvaliandoState _avaliandoState = new();
    private readonly CanceladoState _canceladoState = new();
    private readonly ConcluidoState _concluidoState = new();
    
    public OrcamentoServiceTests()
    {
        _logger = new LoggerFactory().CreateLogger<OrcamentoService>();
        _repositoryMock = new Mock<IOrcamentoRepository>();
        _clienteServiceMock = new Mock<IClienteService>();
        _state = new State(
            aguardandoAvaliacaoState: _aguardandoAvaliacaoState,
            avaliandoState: _avaliandoState,
            canceladoState: _canceladoState,
            concluidoState: _concluidoState);
        _service = new OrcamentoService(_repositoryMock.Object, _logger, _clienteServiceMock.Object, _state);
        
    }

    [Fact]
    public async Task DeveCriarOrcamentoComSucesso()
    {
        var orcamentoRequestDto = OrcamentoMockFactoryTest.ToOrcamentoCriarRequestDto();
        var cliente = ClienteMockFactoryTest.ToClienteEntity();
        PropertyReflection<Cliente>.Set(field: "Id", type: cliente, value: 1);

        _clienteServiceMock.Setup(service => service.GetById(1))
            .ReturnsAsync(cliente);
        
        var orcamentoResponse = await _service.CreateAsync(orcamentoRequestDto);
        
        Assert.NotNull(orcamentoResponse);
        Assert.Equal("Descrição", orcamentoResponse.Descricao);
        Assert.Equal(2500, orcamentoResponse.Valor);
        Assert.Equal(StatusEnum.AguardandoAvaliacao, orcamentoResponse.Status);

        Assert.Equal(1, orcamentoResponse.Cliente.Id);
        Assert.Equal("Nome", orcamentoResponse.Cliente.Nome);
        Assert.Equal("12345678910", orcamentoResponse.Cliente.Cpf);
        Assert.Equal("email@email.com", orcamentoResponse.Cliente.Email);
        Assert.Equal("44900001111", orcamentoResponse.Cliente.Telefone);
    }

    [Fact]
    public async Task DeveMudarStatusDeAguardandoAvaliacaoParaCancelado()
    {
        var orcamento = OrcamentoMockFactoryTest.ToOrcamentoEntity(StatusEnum.AguardandoAvaliacao);
        PropertyReflection<Cliente>.Set(field: "Id", type: orcamento.Cliente, value: 1);
        
        PropertyReflection<Orcamento>.Set(field: "Id", type: orcamento, value: 1);
        
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(orcamento);

        var orcamentoResponse = await _service.CancelarOrcamento(1);
        
        Assert.NotNull(orcamentoResponse);
        Assert.Equal(StatusEnum.Cancelado, orcamentoResponse.Status);
    }

    [Fact]
    public async Task DeveMudarStatusDeAguardandoAvaliacaoParaAvaliarOrcamento()
    {
        var orcamento = OrcamentoMockFactoryTest.ToOrcamentoEntity(StatusEnum.AguardandoAvaliacao);
        PropertyReflection<Cliente>.Set(field: "Id", type: orcamento.Cliente, value: 1);
        
        PropertyReflection<Orcamento>.Set(field: "Id", type: orcamento, value: 1);
        
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(orcamento);

        var orcamentoResponse = await _service.AvaliarOrcamento(1);
        
        Assert.NotNull(orcamentoResponse);
        Assert.Equal(StatusEnum.Avaliando, orcamentoResponse.Status);
        
    }

    [Fact]
    public async Task DeveOcorrerErroAoMudarStatusDeAguardandoAvaliacaoParaConcluido()
    {
        var orcamento = OrcamentoMockFactoryTest.ToOrcamentoEntity(StatusEnum.AguardandoAvaliacao);
        PropertyReflection<Cliente>.Set(field: "Id", type: orcamento.Cliente, value: 1);
        
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(orcamento);

        var excecao = await Assert.ThrowsAsync<BadRequestException>(
            () => _service.ConcluirOrcamento(1)
        );
        
        Assert.NotNull(excecao);
        Assert.Equal("Mudança de status não permitida", excecao.Message);
    }
    
    [Fact]
    public async Task DeveOcorrerErroAoMudarStatusDeCanceladoParaConcluido()
    {
        var orcamento = OrcamentoMockFactoryTest.ToOrcamentoEntity(StatusEnum.Cancelado);
        PropertyReflection<Cliente>.Set(field: "Id", type: orcamento.Cliente, value: 1);
        
        PropertyReflection<Orcamento>.Set(field: "Id", type: orcamento, value: 1);
        
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(orcamento);

        var excecao = await Assert.ThrowsAsync<BadRequestException>(
            () => _service.ConcluirOrcamento(1)
        );
        
        Assert.NotNull(excecao);
        Assert.Equal("Mudança de status não permitida", excecao.Message);
    }
    
    [Fact]
    public async Task DeveOcorrerErroAoMudarStatusDeConcluidoParaCancelado()
    {
        var orcamento = OrcamentoMockFactoryTest.ToOrcamentoEntity(StatusEnum.Concluido);
        PropertyReflection<Cliente>.Set(field: "Id", type: orcamento.Cliente, value: 1);

        PropertyReflection<Orcamento>.Set(field: "Id", type: orcamento, value: 1);
        
        _repositoryMock
            .Setup(repository => repository.FindById(1))
            .ReturnsAsync(orcamento);

        var excecao = await Assert.ThrowsAsync<BadRequestException>(
            () => _service.CancelarOrcamento(1)
        );
        
        Assert.NotNull(excecao);
        Assert.Equal("Mudança de status não permitida", excecao.Message);
    }
    
    
}
