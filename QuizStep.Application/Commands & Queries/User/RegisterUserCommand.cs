using System.ComponentModel.DataAnnotations;
using MediatR;
using QuizStep.Application.DTOs.User;

namespace QuizStep.Application.Commands___Queries.User;

public class RegisterUserCommand: IRequest<RegisterResultDto>
{
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Required] [EmailAddress] public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public string? ConfirmationLink { get; set; }
}