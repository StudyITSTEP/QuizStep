using System.ComponentModel.DataAnnotations;
using MediatR;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.User;

public class LoginUserCommand: IRequest<Result<LoginResultDto>>
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

}