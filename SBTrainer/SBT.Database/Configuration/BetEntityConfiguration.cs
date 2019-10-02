using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    class BetEntityConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Coefficient)
                .IsRequired();

            builder
                .Property(x => x.Money)
                .IsRequired();

            builder
                .Property(x => x.AccountEmail)
                .HasColumnType("varchar(50)");

            builder
                .HasOne(x => x.Game)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.GameId);

            builder
                .HasOne(x => x.Account)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.AccountEmail);
        }
    }
}
