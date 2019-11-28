﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SBT.Database;

namespace SBT.Database.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20191128110515_migration-1")]
    partial class migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SBT.Database.Entities.Account", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(50)");

                    b.Property<float>("Ballance");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("char(32)");

                    b.HasKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SBT.Database.Entities.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountEmail")
                        .HasColumnType("varchar(50)");

                    b.Property<float>("Coefficient");

                    b.Property<int>("GameId");

                    b.Property<float>("Money");

                    b.Property<string>("Result")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AccountEmail");

                    b.HasIndex("GameId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("SBT.Database.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SportDataId")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Team1")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Team2")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("SportDataId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("SBT.Database.Entities.Site", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(10)");

                    b.Property<float?>("Draw");

                    b.Property<float>("FirstWin");

                    b.Property<int>("GameId");

                    b.Property<int>("LastUpdate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<float>("SecondWin");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("SBT.Database.Entities.Sport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SBT.Database.Entities.SportData", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("IsActive");

                    b.Property<string>("SportId")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("SportData");
                });

            modelBuilder.Entity("SBT.Database.Entities.Bet", b =>
                {
                    b.HasOne("SBT.Database.Entities.Account", "Account")
                        .WithMany("Bets")
                        .HasForeignKey("AccountEmail");

                    b.HasOne("SBT.Database.Entities.Game", "Game")
                        .WithMany("Bets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SBT.Database.Entities.Game", b =>
                {
                    b.HasOne("SBT.Database.Entities.SportData", "SportData")
                        .WithMany("Games")
                        .HasForeignKey("SportDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SBT.Database.Entities.Site", b =>
                {
                    b.HasOne("SBT.Database.Entities.Game", "Game")
                        .WithMany("Sites")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SBT.Database.Entities.SportData", b =>
                {
                    b.HasOne("SBT.Database.Entities.Sport", "Sport")
                        .WithMany("SportData")
                        .HasForeignKey("SportId");
                });
#pragma warning restore 612, 618
        }
    }
}
