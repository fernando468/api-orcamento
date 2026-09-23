using Backend.Models;

namespace Backend.Interfaces.Repostiories;

public interface IClienteRepository
{
    Task Save(Cliente cliente);
    Task Update(Cliente cliente);
    Task<Cliente?> FindById(int id);
    Task<IEnumerable<Cliente>> FindAll();
    Task Delete(Cliente cliente);
    Task<Cliente?> FindByCpfAsync(string cpf);
    Task<Cliente?> FindByEmailAsync(string email);
}
