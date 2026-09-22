using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

[Index(nameof(Cpf), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class Cliente
{
    [Key]
    [Required]
    public int Id { get; private set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; private set; }
    
    [Required(ErrorMessage = "CPF é obrigatório")]
    [MinLength(11, ErrorMessage = "CPF deve ter 11 caracteres"), MaxLength(11, ErrorMessage = "CPF deve ter 11 caracteres")]
    public string Cpf { get; private set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [MaxLength(100, ErrorMessage = "Email deve ter no máximo 100 caracteres")]
    public string Email { get; private set; }
    
    [Required(ErrorMessage = "Telefone é obrigatório")]
    [MinLength(10, ErrorMessage = "Telefone deve ter no mínimo 10 caracteres"), MaxLength(11, ErrorMessage = "Telefone deve ter no máximo 11 caracteres")]
    public string Telefone { get; private set; }
    
    [Required(ErrorMessage = "Criado em é obrigatório")]
    public DateTime CriadoEm { get; private set; }
    
    [Required(ErrorMessage = "Atualizado em é obrigatório")]
    public DateTime AtualizadoEm { get; private set; }

    public ICollection<Orcamento> Orcamentos { get; private set; } = [];

    protected Cliente()
    {
        
    }

    public Cliente(string nome, string cpf, string email, string telefone)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void Atualizar(string nome, string cpf, string email, string telefone)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        
        AtualizadoEm = DateTime.Now;
    }
}
