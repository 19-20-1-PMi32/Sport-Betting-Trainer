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

        public DbSet<Sport> Sports { get; set; }

        public DbSet<SportData> SportData { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}
