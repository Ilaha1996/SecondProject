using LMA.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMA.Core.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.Property(x => x.BookId).HasColumnType("int").IsRequired();
        builder.Property(x => x.BorrowerId).HasColumnType("int").IsRequired();
        builder.Property(x => x.LoanDate).HasColumnType("date").IsRequired(); 
        builder.Property(x => x.MustReturnDate).HasColumnType("date").IsRequired();
        builder.Property(x => x.ReturnDate).HasColumnType("date");
        builder.HasOne(x => x.Borrower).WithMany(b => b.Loans).HasForeignKey(x => x.BorrowerId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Book).WithMany(b => b.Loans).HasForeignKey(x => x.BookId).OnDelete( DeleteBehavior.Restrict);

    }
}
