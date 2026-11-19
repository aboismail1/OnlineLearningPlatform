using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.StudentId);

        builder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.DateOfBirth)
            .IsRequired();
    }
}