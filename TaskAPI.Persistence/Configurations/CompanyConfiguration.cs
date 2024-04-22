using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskAPI.Persistence.Entities;

namespace TaskAPI.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<CompanyEntity>
{
    private const int CompanyNameMaxLength = 75;
    private const int CountryNameMaxLength = 62; // Goggle for longest country name
    
    public void Configure(EntityTypeBuilder<CompanyEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.CompanyName)
            .HasMaxLength(CompanyNameMaxLength)
            .IsRequired();

        builder
            .Property(c => c.BossId)
            .IsRequired();

        builder.Property(c => c.Country)
            .HasMaxLength(CountryNameMaxLength)
            .IsRequired();
    }
}