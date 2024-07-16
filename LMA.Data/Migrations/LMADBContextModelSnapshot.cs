﻿// <auto-generated />
using System;
using LMA.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMA.Data.Migrations
{
    [DbContext(typeof(LMADBContext))]
    partial class LMADBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMA.Core.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LMA.Core.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BorrowedTimes")
                        .HasColumnType("int");

                    b.Property<int?>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("PublishedYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LMA.Core.Entities.BookAuthors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("LMA.Core.Entities.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<bool>("LateReturner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("LMA.Core.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("MustReturnDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("LMA.Core.Entities.Book", b =>
                {
                    b.HasOne("LMA.Core.Entities.Borrower", "Borrower")
                        .WithMany("Books")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("LMA.Core.Entities.BookAuthors", b =>
                {
                    b.HasOne("LMA.Core.Entities.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMA.Core.Entities.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LMA.Core.Entities.Loan", b =>
                {
                    b.HasOne("LMA.Core.Entities.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LMA.Core.Entities.Borrower", "Borrower")
                        .WithMany("Loans")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("LMA.Core.Entities.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("LMA.Core.Entities.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("Loans");
                });

            modelBuilder.Entity("LMA.Core.Entities.Borrower", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}