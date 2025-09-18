namespace QuizStep.Core.Entities;

public class TestResult
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
    public decimal Score { get; set; }
}