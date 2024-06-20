﻿using ChellengeBackEnd_APIContas.Models;
using Microsoft.EntityFrameworkCore;

namespace ChellengeBackEnd_APIContas.Data;

public class ChallengeContext : DbContext
{
    public ChallengeContext(DbContextOptions<ChallengeContext> opts) : base(opts)
    { 
    
    }

    public DbSet<Receita>? Receita { get; set; }

    public DbSet<Despesa>? Despesa { get; set; }



}
