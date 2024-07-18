using ChellengeBackEnd_APIContas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChellengeBackEnd_APIContas.Data;

public class ChallengeContext : IdentityDbContext<Usuario>
{
    public ChallengeContext(DbContextOptions<ChallengeContext> opts) : base(opts)
    { 
    
    }

    public DbSet<Receita> Receita { get; set; }

    public DbSet<Despesa> Despesa { get; set; }

    public DbSet<Categoria> Categoria {  get; set; }


}
 