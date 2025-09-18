namespace QuizStep.Core.Entities;

public class Answer
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public IEnumerable<Question> Questions { get; set; } = null!;
}