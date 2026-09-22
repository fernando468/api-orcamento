using Backend.Models;

namespace Backend.Interfaces.Repostiories;

public interface IOrcamentoRepository
{
    Task Save(Orcamento orcamento);
    Task Update(Orcamento orcamento);
    Task<Orcamento?> FindById(int id);
    Task<IEnumerable<Orcamento>> FindAll();
}
