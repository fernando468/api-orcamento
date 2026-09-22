namespace Backend.Dtos.Responses;

public record ClienteResponseDto(
    int Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email
);
