using System.ComponentModel.DataAnnotations;

namespace QuizStep.Application.DTOs.User;

public class RefreshTokenDto
{
    [Required]
    public string? UserId { get; set; }
    [Required]
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
}