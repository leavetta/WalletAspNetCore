﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WalletAspNetCore.DataBaseOperations.EFStructures;

#nullable disable

namespace WalletAspNetCore.DataBaseOperations.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Balance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsIncome")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FK_Transactions_Category_CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FK_Transactions_User_UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FK_Transactions_Category_CategoryId");

                    b.HasIndex("FK_Transactions_User_UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("FK_Balance_User_UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FK_Balance_User_UserId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Transaction", b =>
                {
                    b.HasOne("WalletAspNetCore.Models.Entities.Category", "CategoryNavigation")
                        .WithMany("Transactions")
                        .HasForeignKey("FK_Transactions_Category_CategoryId")
                        .IsRequired();

                    b.HasOne("WalletAspNetCore.Models.Entities.User", "UserNavigation")
                        .WithMany("Transactions")
                        .HasForeignKey("FK_Transactions_User_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.User", b =>
                {
                    b.HasOne("WalletAspNetCore.Models.Entities.Balance", "BalanceNavigation")
                        .WithOne("UserNavigation")
                        .HasForeignKey("WalletAspNetCore.Models.Entities.User", "FK_Balance_User_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BalanceNavigation");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Balance", b =>
                {
                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WalletAspNetCore.Models.Entities.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
