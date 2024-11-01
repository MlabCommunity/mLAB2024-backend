﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizBackend.Domain.Entities;

namespace QuizBackend.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(u => u.OwnedQuizzes)
            .WithOne(q => q.Owner)
            .HasForeignKey(q => q.OwnerId);

        builder
            .HasMany(q => q.ParticipatedQuizzes)
            .WithMany(u => u.Participants)
            .UsingEntity<QuizParticipation>(
                j => j
                    .HasOne(qp => qp.Quiz)
                    .WithMany()
                    .HasForeignKey(qp => qp.QuizId)
                    .OnDelete(DeleteBehavior.Cascade), 
                j => j
                    .HasOne(qp => qp.Participant)
                    .WithMany()
                    .HasForeignKey(qp => qp.ParticipantId),
                j =>
                {
                    j.ToTable("QuizzesParticipations");
                    j.HasKey(qp => qp.Id);
                    j.Property(qp => qp.ParticipationDateUtc).IsRequired();
                });

        builder.HasQueryFilter(u => !u.IsDeleted);
    }
}