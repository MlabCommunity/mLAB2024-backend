using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure (EntityTypeBuilder<QuizResult> builder)
    {
        builder.HasKey(qr => qr.Id);
        builder.Property(qr => qr.ScorePercentage).IsRequired();

        builder
            .HasOne(qr => qr.QuizParticipation)
            .WithOne(qr => qr.QuizResult)
            .HasForeignKey<QuizResult>(qr => qr.QuizParticipationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}