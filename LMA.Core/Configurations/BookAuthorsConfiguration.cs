using LMA.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMA.Core.Configurations;

public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthors>
{
    public void Configure(EntityTypeBuilder<BookAuthors> builder)
    {
        builder.HasOne(x => x.Book).WithMany(x => x.BookAuthors);
        builder.HasOne(x => x.Author).WithMany(x => x.BookAuthors);
    }
}
