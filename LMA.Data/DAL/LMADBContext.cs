using LMA.Core.Configurations;
using LMA.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMA.Data.DAL;

public class LMADBContext : DbContext
{
    public DbSet<Book> Books { get; set; }  
    public DbSet <Author> Authors { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<BookAuthors> BookAuthors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookAuthorsConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=ILAHA\\SQLEXPRESS01;Database=LibraryManagementAppDB ;Trusted_Connection=True;TrustServerCertificate=True");
        base.OnConfiguring(optionsBuilder);
    }

}
