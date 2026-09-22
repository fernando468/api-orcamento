namespace Backend.Dtos.Responses;

public record OrcamentoResponseDto(int Id, ClienteResponseDto Cliente, double Valor, string Descricao);
