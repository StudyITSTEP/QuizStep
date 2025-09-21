namespace QuizStep.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<Quiz>  Quizzes { get; set; } = null!;
}