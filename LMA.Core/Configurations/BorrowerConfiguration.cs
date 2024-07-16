using LMA.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMA.Core.Configurations;

public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
{
    public void Configure(EntityTypeBuilder<Borrower> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(60);
        builder.Property(x => x.Email).IsRequired(false).HasMaxLength(70);
        builder.Property(x => x.LateReturner).IsRequired().HasDefaultValue(false);
    }
}