using QuizStep.Core.Enums;

namespace QuizStep.Application.DTOs.Quiz;

public class QuizDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int TotalQuestions { get; set; }
    public string CreatorName { get; set; }
    public string CreatorEmail { get; set; }
    public int TotalParticipants { get; set; }
    public QuizAccess Access { get; set; }
    public decimal AverageScore { get; set; }
}