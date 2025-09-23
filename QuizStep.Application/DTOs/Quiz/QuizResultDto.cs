namespace QuizStep.Application.DTOs.Quiz;

public class QuizResultDto
{
    public string UserId { get; set; }
    public int TestId { get; set; }
    public decimal Score { get; set; }
}