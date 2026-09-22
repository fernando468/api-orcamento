using Backend.Dtos.Requests;
using Backend.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrcamentoController : ControllerBase
{
    private readonly IOrcamentoService _orcamentoService;

    public OrcamentoController(IOrcamentoService orcamentoService)
    {
        _orcamentoService = orcamentoService;
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromBody] OrcamentoCriarRequestDto orcamentoCriarRequestDto)
    {
        var orcamentoCriadoResponse = await _orcamentoService.CreateAsync(orcamentoCriarRequestDto);
        
        return CreatedAtAction(nameof(FindById), 
            new { id = orcamentoCriadoResponse.Id }, 
            orcamentoCriadoResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindById(int id)
    {
        var orcamento = await _orcamentoService.FindByIdAsync(id);
        
        return Ok(orcamento);
    }

    [HttpGet]
    public async Task<IActionResult> FindAllAsync()
    {
        var listaOrcamento = await _orcamentoService.FindAllAsync();
        
        return Ok(listaOrcamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] OrcamentoUpdateRequestDto orcamentoUpdateRequestDto)
    {
        var orcamentoAtualizado = await _orcamentoService.UpdateAsync(id, orcamentoUpdateRequestDto);
        
        return Ok(orcamentoAtualizado);
    }

    [HttpPut("{id}/cancelar")]
    public async Task<IActionResult> CancelarOrcamento(int id)
    {
        var cancelarOrcamento = await _orcamentoService.CancelarOrcamento(id);
        
        return Ok(cancelarOrcamento);
    }


    [HttpPut("{id}/concluir")]
    public async Task<IActionResult> ConcluirOrcamento(int id)
    {
        var cancelarOrcamento = await _orcamentoService.ConcluirOrcamento(id);
        
        return Ok(cancelarOrcamento);
    }
    
    [HttpPut("{id}/avaliar")]
    public async Task<IActionResult> AvaliarOrcamento(int id)
    {
        var cancelarOrcamento = await _orcamentoService.AvaliarOrcamento(id);
        
        return Ok(cancelarOrcamento);
    }
}
