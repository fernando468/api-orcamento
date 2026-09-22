using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Orcamento
{
    [Key]
    [Required]
    public int Id { get; private set; }
    
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [MaxLength(200, ErrorMessage = "Descricao deve ter no máximo 200 caracteres")]
    public string Descricao { get; private set; }
    
    [Required(ErrorMessage = "Cliente é obrigatório")]
    public Cliente Cliente { get; private set; }
    
    [Required(ErrorMessage = "Status é obrigatório")]
    public StatusEnum Status { get; private set; }

    [Required(ErrorMessage = "Valor é obrigatório")]
    public double Valor { get; private set; }
    
    [Required(ErrorMessage = "Criado em é obrigatório")]
    public DateTime CriadoEm { get; private set; }
    
    [Required(ErrorMessage = "Atualizado em é obrigatório")]
    public DateTime AtualizadoEm { get; private set; }

    protected Orcamento()
    {
        
    }
    
    public Orcamento(string descricao, Cliente cliente, double valor, StatusEnum status)
    {
        Descricao = descricao;
        Cliente = cliente;
        Valor = valor;
        Status = status;
        
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }
    
    public void Atualizar(string descricao, double valor, StatusEnum status)
    {
        Descricao = descricao;
        Valor = valor;
        Status = status;
        
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarStatus(StatusEnum status)
    {
        Status = status;
        
        AtualizadoEm = DateTime.UtcNow;
    }
    
}
