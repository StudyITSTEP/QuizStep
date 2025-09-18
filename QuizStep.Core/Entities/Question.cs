namespace QuizStep.Core.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public IEnumerable<Answer> Answers { get; set; } = null!;
    
}