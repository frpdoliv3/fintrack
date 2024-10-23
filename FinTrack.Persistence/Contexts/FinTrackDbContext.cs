﻿using FinTrack.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Persistence.Contexts;

public partial class FinTrackDbContext : IdentityDbContext<EFUser>
{
    public FinTrackDbContext(DbContextOptions<FinTrackDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SetupCountryConstraints(modelBuilder);
        SetupCurrencyConstraints(modelBuilder);
        //SetupSecurityConstraints(modelBuilder);
    }
}