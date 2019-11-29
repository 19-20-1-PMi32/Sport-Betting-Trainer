using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    class SportDataEntityConfiguration : IEntityTypeConfiguration<SportData>
    {
        public void Configure(EntityTypeBuilder<SportData> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .IsRequired();

            builder
                .Property(x => x.Group)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(x => x.Details)
                .HasColumnType("varchar(75)")
                .IsRequired();

            builder
                .Property(x => x.Title)
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .Property(x => x.SportId)
                .HasColumnType("varchar(10)");

            builder
                .HasOne(x => x.Sport)
                .WithMany(x => x.SportData)
                .HasForeignKey(x => x.SportId);

            builder
                .HasMany(x => x.Games)
                .WithOne(x => x.SportData);
        }
    }
}
