using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskAPI.Persistence.Entities;

namespace TaskAPI.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int NameMaxLength = 50;
    private const int PhoneNumberMaxLength = 20;
    
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Name)
            .HasMaxLength(NameMaxLength)
            .IsRequired();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .Property(u => u.PhoneNumber)
            .HasMaxLength(PhoneNumberMaxLength)
            .IsRequired();

        builder
            .Property(u => u.CompanyId)
            .IsRequired();

        builder
            .Property(u => u.IsBoss)
            .IsRequired();
    }
}