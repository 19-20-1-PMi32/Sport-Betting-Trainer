using Microsoft.EntityFrameworkCore;
using SBT.Database.Configuration;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;

namespace SBT.Database
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SportEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SportDataEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GameEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SiteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        }

        DbSet<Sport> Sports { get; set; }

        DbSet<SportData> SportData { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Site> Sites { get; set; }

        DbSet<Bet> Bets { get; set; }

        DbSet<Account> Accounts { get; set; }
    }
}
