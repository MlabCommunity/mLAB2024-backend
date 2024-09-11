using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class QuizParticipationConfiguration : IEntityTypeConfiguration<QuizParticipation>
{
    public void Configure(EntityTypeBuilder<QuizParticipation> builder)
    {
        builder.HasKey(qp => qp.Id);
        builder.Property(qp => qp.ParticipationDateUtc).IsRequired();

        builder
            .HasOne(qp => qp.Quiz)
            .WithMany(q => q.Participants)
            .HasForeignKey(qp => qp.QuizId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(qp => qp.UserAnswers)
               .WithOne(ua => ua.QuizParticipation)
               .HasForeignKey(ua => ua.QuizParticipationId);

        builder
            .HasMany(qp => qp.UserAnswers)
            .WithOne(ua => ua.QuizParticipation)
            .HasForeignKey(ua => ua.QuizParticipationId);

        builder
            .Property(qp => qp.Status)
            .HasConversion(new EnumToStringConverter<QuizParticipationStatus>())
            .IsRequired();

        builder
            .Property(qp => qp.CompletionTime)
            .IsRequired(false);

        builder
            .HasDiscriminator<string>("ParticipationType")
            .HasValue<RegisteredUserParticipation>("RegisteredUser")
            .HasValue<GuestUserParticipation>("GuestUser");
    }
}