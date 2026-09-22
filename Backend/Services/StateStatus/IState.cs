using Backend.Enums;

namespace Backend.Services.StateStatus;

public interface IState
{
    StatusEnum AguardandoAvaliacao();
    StatusEnum Avaliando();
    StatusEnum Cancelado();
    StatusEnum Concluido();
}
