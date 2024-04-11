using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ApplicationHandler.Domain.Entities;

namespace ApplicationHandler.Infrastructure.Database.Configurations;

public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.Property(e => e.Text).HasMaxLength(80).IsRequired();
    }
}
