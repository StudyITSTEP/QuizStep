using System.ComponentModel.DataAnnotations;

namespace QuizStep.Core.Entities;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? UserId { get; set; }
    public User User { get; set; }
    [Required]
    public string? Hash { get; set; }
    [Required]
    public string? Salt { get; set; }
    public DateTime Expires { get; set; }
}