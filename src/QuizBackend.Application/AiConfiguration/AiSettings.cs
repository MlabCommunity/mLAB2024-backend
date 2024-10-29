namespace QuizBackend.Application.AiConfiguration;

public class AiSettings
{
    public required string Type { get; set; }
    public required string Key { get; set; }
    public required string Endpoint { get; set; }
    public required string Model { get; set; }
}