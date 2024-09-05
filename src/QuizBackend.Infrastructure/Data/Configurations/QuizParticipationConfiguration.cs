using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;

public class QuizParticipationConfiguration : IEntityTypeConfiguration<QuizParticipation>
{
    public void Configure(EntityTypeBuilder<QuizParticipation> builder)
    {
        builder.HasKey(qp => qp.Id);
        builder.Property(qp => qp.ParticipationDateUtc).IsRequired();

        builder.HasOne(qp => qp.Quiz)
            .WithMany()
            .HasForeignKey(qp => qp.QuizId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(qp => qp.Participant)
            .WithMany()
            .HasForeignKey(qp => qp.ParticipantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}