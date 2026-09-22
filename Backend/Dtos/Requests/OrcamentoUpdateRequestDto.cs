using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public record OrcamentoUpdateRequestDto(
    [Required(ErrorMessage = "Descricao é obrigatório")]
    [MaxLength(200, ErrorMessage = "Descricao deve ter no máximo 200 caracteres")]
    string Descricao, 
    
    [Required(ErrorMessage = "Valor é obrigatório")]
    [Range(0.00, 999999.99, ErrorMessage = "Valor deve estar entre 0,01 e 999.999,99")]
    double Valor);
