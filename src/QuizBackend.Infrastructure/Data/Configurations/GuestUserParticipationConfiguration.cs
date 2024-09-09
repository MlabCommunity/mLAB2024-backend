using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;
public class GuestUserParticipationConfiguration : IEntityTypeConfiguration<GuestUserParticipation>
{
    public void Configure(EntityTypeBuilder<GuestUserParticipation> builder)
    {
        builder.HasBaseType<QuizParticipation>();
    }
}