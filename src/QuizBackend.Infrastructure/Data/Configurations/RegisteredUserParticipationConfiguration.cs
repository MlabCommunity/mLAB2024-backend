using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class RegisteredUserParticipationConfiguration : IEntityTypeConfiguration<RegisteredUserParticipation>
{
    public void Configure(EntityTypeBuilder<RegisteredUserParticipation> builder)
    {
        builder.HasBaseType<QuizParticipation>();

        builder.HasOne(rup => rup.Participant)
               .WithMany()
               .HasForeignKey(rup => rup.ParticipantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}