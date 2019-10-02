using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBT.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBT.Database.Configuration
{
    class SiteEntityConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnType("varchar(10)")
                .IsRequired();

            builder
                .Property(x => x.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder
                .Property(x => x.LastUpdate)
                .IsRequired();

            builder
                .Property(x => x.FirstWin)
                .IsRequired();

            builder
                .Property(x => x.SecondWin)
                .IsRequired();

            builder
                .Property(x => x.Draw)
                .IsRequired(false);

            builder
                .HasOne(x => x.Game)
                .WithMany(x => x.Sites)
                .HasForeignKey(x => x.GameId);
        }
    }
}
