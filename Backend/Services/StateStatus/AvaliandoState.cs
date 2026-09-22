using Backend.Enums;
using Backend.Exceptions;

namespace Backend.Services.StateStatus;

public class AvaliandoState : IState
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
        return StatusEnum.Cancelado;
    }

    public StatusEnum Concluido()
    {
        return StatusEnum.Concluido;
    }
}
