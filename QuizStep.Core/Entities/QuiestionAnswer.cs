namespace QuizStep.Core.Entities;

public class QuestionAnswer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public int AnswerId { get; set; }
    public Answer Answer { get; set; } = null!;
    public bool IsCorrect { get; set; }
}