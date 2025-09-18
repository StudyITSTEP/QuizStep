using System.ComponentModel.DataAnnotations;
using MediatR;
using QuizStep.Application.DTOs.User;

namespace QuizStep.Application.Commands___Queries.User;

public class LoginUserCommand: IRequest<LoginDto>
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}