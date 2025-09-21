using MediatR;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.User;

public record ConfirmEmailCommand(string? Token, string? Email): IRequest<Result>
{
}