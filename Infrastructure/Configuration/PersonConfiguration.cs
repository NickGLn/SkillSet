using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSet.Domain;

namespace Infrastructure.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(n => n.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(n => n.DisplayName)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(p => p.Skills)
            .WithOne(s => s.Person)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
