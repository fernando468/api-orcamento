using Backend.Contexts;
using Backend.Interfaces;
using Backend.Interfaces.Repostiories;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repostiories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Save(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<Cliente?> FindById(int id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Id == id);
    }

    public async Task<IEnumerable<Cliente>> FindAll()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task Delete(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
    
}
