using QuizBackend.Domain.Common;

namespace QuizBackend.Domain.Entities;

public class QuizResult : BaseEntity
{
    public Guid QuizParticipationId { get; set; }
    public QuizParticipation QuizParticipation { get; set; } = null!;
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public double ScorePercentage { get; set; }
    public DateTime CalculatedAt { get; set; }
}