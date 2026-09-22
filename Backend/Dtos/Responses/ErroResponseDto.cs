namespace Backend.Dtos.Responses;

public record ErroResponseDto(
    int StatusCode,
    string Descricao,
    DateTime DateTime,
    string Path
);
