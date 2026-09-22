using Backend.Enums;
using Backend.Exceptions;
using Backend.Models;

namespace Backend.Services.StateStatus;

public class State
{
    private readonly AguardandoAvaliacaoState _aguardandoAvaliacaoState;
    private readonly AvaliandoState _avaliandoState;
    private readonly CanceladoState _canceladoState;
    private readonly ConcluidoState _concluidoState;

    public State(AguardandoAvaliacaoState aguardandoAvaliacaoState, 
        AvaliandoState avaliandoState, 
        CanceladoState canceladoState, 
        ConcluidoState concluidoState)
    {
        _aguardandoAvaliacaoState = aguardandoAvaliacaoState;
        _avaliandoState = avaliandoState;
        _canceladoState = canceladoState;
        _concluidoState = concluidoState;
    }

    public StatusEnum ProcessarMudancaStatus(Orcamento orcamento, StatusEnum proximoStatus)
    {
        var statusAtual = orcamento.Status;
        var stateAtual = EscolherState(statusAtual);
        var novoStatus = MudarStatus(stateAtual, proximoStatus);
        return novoStatus;
    }
    
    private StatusEnum MudarStatus(IState stateAtual, StatusEnum proximoStatus)
    {
        var novoStatus = proximoStatus switch
        {
            StatusEnum.AguardandoAvaliacao => stateAtual.AguardandoAvaliacao(),
            StatusEnum.Avaliando => stateAtual.Avaliando(),
            StatusEnum.Cancelado => stateAtual.Cancelado(),
            StatusEnum.Concluido => stateAtual.Concluido(),
            _ => throw new Exception("Status desejado inválido")
        };
        
        return novoStatus;
    }
    
    private IState EscolherState(StatusEnum statusAtual)
    {

        return statusAtual switch
        {
            StatusEnum.AguardandoAvaliacao => _aguardandoAvaliacaoState,
            StatusEnum.Avaliando => _avaliandoState,
            StatusEnum.Cancelado => _canceladoState,
            StatusEnum.Concluido => _concluidoState,
            _ => throw new BadRequestException("Status não encontrado")
        };
    }
}
