using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;

public class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(qr => qr.QuizParticipation)
            .WithOne(qr => qr.QuizResult)
            .HasForeignKey<QuizResult>(qr => qr.QuizParticipationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}