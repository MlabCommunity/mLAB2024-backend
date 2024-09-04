using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizBackend.Domain.Entities;
using QuizBackend.Domain.Enums;


namespace QuizBackend.Infrastructure.Data.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);

        builder
            .Property(q => q.QuestionType)
            .HasConversion(new EnumToStringConverter<QuestionType>());

        builder
           .HasMany(q => q.Answers)
           .WithOne(a => a.Question)
           .HasForeignKey(a => a.QuestionId);
    }
}