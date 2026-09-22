using Backend.Dtos.Responses;

namespace Backend.Mappers;

public abstract class ErroMapper
{
    public static ErroResponseDto ToDto(int statusCode, string descricao, String path)
    {
        return new ErroResponseDto(
            Path: path,
            Descricao: descricao,
            DateTime: DateTime.Now,
            StatusCode: statusCode
            );
    }
}
