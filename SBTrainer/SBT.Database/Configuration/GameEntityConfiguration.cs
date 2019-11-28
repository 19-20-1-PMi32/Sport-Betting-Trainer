using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Team1)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(x => x.Team2)
                .HasColumnType("varchar(20)")
                .IsRequired();

			builder
				.Property(x => x.CommenceTime)
				.IsRequired();

			builder
                .Property(x => x.SportDataId)
                .HasColumnType("varchar(45)")
                .IsRequired();

            builder
                .HasOne(x => x.SportData)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.SportDataId);

            builder
                .HasMany(x => x.Sites)
                .WithOne(x => x.Game);

            builder
                .HasMany(x => x.Bets)
                .WithOne(x => x.Game);
        }
    }
}
