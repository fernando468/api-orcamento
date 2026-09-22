using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public record OrcamentoUpdateRequestDto(
    [Required(ErrorMessage = "Descricao é obrigatório")]
    [MaxLength(200, ErrorMessage = "Descricao deve ter no máximo 200 caracteres")]
    string Descricao, 
    
    [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve estar entre 0,01 e 999.999,99")]
    double Valor);
