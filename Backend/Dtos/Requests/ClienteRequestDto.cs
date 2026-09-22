using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos.Requests;

public record ClienteRequestDto(
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres"), MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    string Nome,
    
    [Required(ErrorMessage = "CPF é obrigatório")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter apenas números e ter 11 dígitos")]
    string Cpf,
    
    [Required(ErrorMessage = "Telefone é obrigatório")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Telefone deve conter apenas números e ter 10 ou 11 dígitos")]
    string Telefone,
    
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Informe um email válido")]
    [MaxLength(100, ErrorMessage = "Email deve ter no máximo 100 caracteres")]
    string Email
);
