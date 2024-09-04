using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Infrastructure.Data.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(q => q.Questions)
            .WithOne(q => q.Quiz)
            .HasForeignKey(q => q.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(q => q.Owner)
            .WithMany(u => u.OwnedQuizzes)
            .HasForeignKey(q => q.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(q => q.Participants)
            .WithMany(u => u.ParticipatedQuizzes)
            .UsingEntity<QuizParticipation>();

        builder
            .Property(q => q.Status)
            .HasConversion(new EnumToStringConverter<Status>());

        builder
            .Property(q => q.Availability)
            .HasConversion(new EnumToStringConverter<Availability>());
    }
}