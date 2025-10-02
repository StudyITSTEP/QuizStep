using System.ComponentModel.DataAnnotations;
using MediatR;

namespace QuizStep.Application.DTOs.User;

public class RefreshTokenDto
{
    [Required]
    public string? UserId { get; set; }
    [Required]
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
}