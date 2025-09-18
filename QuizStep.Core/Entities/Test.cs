using System.ComponentModel.DataAnnotations;

namespace QuizStep.Core.Entities;

public class Test
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
    public TestAccess? Access { get; set; }
    public List<TestResult> TestsResults { get; set; } = null!;
    public List<Question> Questions { get; set; } = null!;
}