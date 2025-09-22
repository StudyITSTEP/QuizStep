namespace QuizStep.Core.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public IEnumerable<Answer> Answers { get; set; } = null!;
    
}