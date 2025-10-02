namespace QuizStep.Application.DTOs.Quiz;

public class QuizResultDto
{
    public string UserId { get; set; }
    public int QuizId { get; set; }
    public decimal Score { get; set; }
}