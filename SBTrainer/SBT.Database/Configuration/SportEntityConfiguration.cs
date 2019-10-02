using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    class SportEntityConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("varchar(10)");

            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder
                .HasMany(x => x.SportData)
                .WithOne(x => x.Sport);
        }
    }
}
