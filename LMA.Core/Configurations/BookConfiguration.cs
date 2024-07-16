using LMA.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMA.Core.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.PublishedYear).IsRequired();
        builder.Property(x => x.BorrowerId).IsRequired(false).HasColumnType("int");
        builder.Property(x => x.IsAvailable).HasDefaultValue(true).IsRequired();
        builder.Property(x => x.BorrowedTimes).HasColumnType("int").IsRequired(false);
        builder.HasOne(x => x.Borrower).WithMany(x =>x.Books).HasForeignKey(x => x.BorrowerId).OnDelete(DeleteBehavior.Cascade);
    }
}
