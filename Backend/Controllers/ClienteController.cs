using Backend.Dtos.Requests;
using Backend.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ClienteRequestDto clienteRequestDto)
    {
        var clienteCriadoResponse = await _clienteService.CreateAsync(clienteRequestDto);
        
        return CreatedAtAction(nameof(FindById), 
        new { id = clienteCriadoResponse.Id } , 
        clienteCriadoResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] ClienteRequestDto clienteRequestDto)
    {
        var clienteAtualizadoResponse = await _clienteService.UpdateAsync(id, clienteRequestDto);
        
        return Ok(clienteAtualizadoResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(int id)
    {
        var clientePorIdResponse = await _clienteService.FindByIdAsync(id);
        
        return Ok(clientePorIdResponse);
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var clienteListaResponse = await _clienteService.FindAllAsync();
        
        return Ok(clienteListaResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        await _clienteService.DeleteById(id);
        return NoContent();
    }
}
