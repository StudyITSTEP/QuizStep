namespace QuizStep.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<Test>  Tests { get; set; } = null!;
}