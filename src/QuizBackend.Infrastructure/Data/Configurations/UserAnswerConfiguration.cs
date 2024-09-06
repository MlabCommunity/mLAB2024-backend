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

        builder
            .HasOne(ua => ua.Question)
            .WithMany(q => q.UserAnswers)
            .HasForeignKey(ua => ua.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(ua => ua.Answer)
            .WithMany(a => a.UserAnswers)
            .HasForeignKey(ua => ua.AnswerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}