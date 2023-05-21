﻿// <auto-generated />
using System;
using LaborProjectOOP.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LaborProjectOOP.Database.Models.AuthorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors", (string)null);
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.BookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorFK")
                        .HasColumnType("integer");

                    b.Property<int?>("CatalogFK")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("boolean");

                    b.Property<int?>("OrderFK")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorFK");

                    b.HasIndex("CatalogFK");

                    b.HasIndex("OrderFK");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.CatalogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Catalogs", (string)null);
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.LibrarianEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Salary")
                        .HasColumnType("integer");

                    b.Property<byte>("WorkExperience")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Librarians", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAdmin = true,
                            Login = "admin",
                            Password = "C4CA4238A0B923820DCC509A6F75849B",
                            Salary = 0,
                            WorkExperience = (byte)0
                        });
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CustomerFK")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActual")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CustomerFK");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.BookEntity", b =>
                {
                    b.HasOne("LaborProjectOOP.Database.Models.AuthorEntity", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorFK")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LaborProjectOOP.Database.Models.CatalogEntity", "Catalog")
                        .WithMany("Books")
                        .HasForeignKey("CatalogFK")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LaborProjectOOP.Database.Models.OrderEntity", "Order")
                        .WithMany("Books")
                        .HasForeignKey("OrderFK")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Author");

                    b.Navigation("Catalog");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.OrderEntity", b =>
                {
                    b.HasOne("LaborProjectOOP.Database.Models.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.AuthorEntity", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.CatalogEntity", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.CustomerEntity", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LaborProjectOOP.Database.Models.OrderEntity", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
