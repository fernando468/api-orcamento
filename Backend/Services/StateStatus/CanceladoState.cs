using Backend.Enums;
using Backend.Exceptions;

namespace Backend.Services.StateStatus;

public class CanceladoState : IState
{
    public StatusEnum AguardandoAvaliacao()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }

    public StatusEnum Avaliando()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }

    public StatusEnum Cancelado()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }

    public StatusEnum Concluido()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }
}
