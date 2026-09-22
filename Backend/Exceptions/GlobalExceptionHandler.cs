using Backend.Mappers;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Ocorreu uma exceção não tratada: {Message}",
            exception.Message);

        var (statusCode, descricao) = exception switch
        {
            NotFoundException =>
                (StatusCodes.Status404NotFound, exception.Message),

            _ =>
                (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            ErroMapper.ToDto(statusCode, descricao, httpContext.Request.Path.ToString()),
            cancellationToken);

        return true;
    }
}
