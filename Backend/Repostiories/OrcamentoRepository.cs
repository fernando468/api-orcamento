using Backend.Contexts;
using Backend.Interfaces.Repostiories;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repostiories;

public class OrcamentoRepository : IOrcamentoRepository
{
    private readonly AppDbContext _context;

    public OrcamentoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task Save(Orcamento orcamento)
    {
        await _context.Orcamentos.AddAsync(orcamento);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Orcamento orcamento)
    {
        _context.Orcamentos.Update(orcamento);
        await _context.SaveChangesAsync();
    }

    public async Task<Orcamento?> FindById(int id)
    {
        return await _context.Orcamentos.Include(orcamento => orcamento.Cliente).FirstOrDefaultAsync(orcamento => orcamento.Id == id);
    }

    public async Task<IEnumerable<Orcamento>> FindAll()
    {
        return await _context.Orcamentos.Include(orcamento => orcamento.Cliente).ToListAsync();
    }

}
