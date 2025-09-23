namespace QuizStep.Core.Entities;

public class QuizResult
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public decimal Score { get; set; }
}