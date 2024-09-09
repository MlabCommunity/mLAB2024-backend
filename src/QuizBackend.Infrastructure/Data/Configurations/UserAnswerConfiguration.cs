using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(ua => ua.CreatedAtUtc).IsRequired();

        builder
            .HasOne(ua => ua.QuizParticipation)
            .WithMany(qp => qp.UserAnswers)
            .HasForeignKey(ua => ua.QuizParticipationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}