using System.ComponentModel.DataAnnotations;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.DTOs.User;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}