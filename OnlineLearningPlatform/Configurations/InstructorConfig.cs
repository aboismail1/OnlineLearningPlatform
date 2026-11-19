using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.Entities;

namespace OnlineLearningPlatform.Configurations;

public class InstructorConfig : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(i => i.InstructorId);

        builder.Property(i => i.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(i => i.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(i => i.Bio)
            .HasMaxLength(500);

        builder.Property(i => i.JoinedDate)
            .IsRequired();
    }
}