using Backend.Enums;
using Backend.Exceptions;

namespace Backend.Services.StateStatus;

public class AguardandoAvaliacaoState : IState
{
    public StatusEnum AguardandoAvaliacao()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }

    public StatusEnum Avaliando()
    {
        return StatusEnum.Avaliando;
    }

    public StatusEnum Cancelado()
    {
        return StatusEnum.Cancelado;
    }

    public StatusEnum Concluido()
    {
        throw new BadRequestException("Mudança de status não permitida");
    }
}
