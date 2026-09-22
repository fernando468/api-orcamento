using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Orcamento> Orcamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .Property(c => c.CriadoEm)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        
        modelBuilder.Entity<Orcamento>()
            .Property(p => p.CriadoEm)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}
