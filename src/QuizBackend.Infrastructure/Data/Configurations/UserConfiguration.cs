using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.OwnedQuizzes)
            .WithOne(q => q.Owner)
            .HasForeignKey(q => q.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.ParticipatedQuizzes)
              .WithOne(rup => rup.Participant)
              .HasForeignKey(rup => rup.ParticipantId)
              .OnDelete(DeleteBehavior.NoAction);
    }
}