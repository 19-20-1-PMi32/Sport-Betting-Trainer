using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    public class AccountEntityConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasKey(x => x.Email);

            builder
                .Property(x => x.Email)
                .HasColumnType("varchar(50)");

            builder
                .Property(x => x.Ballance)
                .IsRequired();

            builder
                .Property(x => x.PasswordHash)
                .HasColumnType("char(32)")
                .IsRequired();

            builder
                .HasMany(x => x.Bets)
                .WithOne(x => x.Account);
        }
    }
}
