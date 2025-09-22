using System.ComponentModel.DataAnnotations;
using QuizStep.Core.Enums;

namespace QuizStep.Core.Entities;

public class Quiz
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    [Required]
    public string? CreatorId { get; set; }
    public User Creator { get; set; }
    public QuizAccess? Access { get; set; }
    public int? AccessCode { get; set; }
    public List<QuizResult> TestsResults { get; set; } = null!;
    public List<Question> Questions { get; set; } = null!;
}