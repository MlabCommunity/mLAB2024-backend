namespace QuizBackend.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? Revoked { get; set; }
}