using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dtos.Requests;

public record OrcamentoCriarRequestDto(
    [Required(ErrorMessage = "ClienteId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "ClienteId deve ser maior que zero")]
    int ClienteId,
    
    [Required(ErrorMessage = "Descricao é obrigatória")]
    [MaxLength(200, ErrorMessage = "Descricao deve ter no máximo 200 caracteres")]
    string Descricao, 
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve estar entre 0,01 e 999.999,99")]
    double Valor);
