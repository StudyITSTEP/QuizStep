using MediatR;
using QuizStep.Application.DTOs.User;
using QuizStep.Core.Primitives;

namespace QuizStep.Application.Commands___Queries.User;

public record RefreshTokenCommand(string RefreshToken, string UserId): IRequest<Result<RefreshTokenDto?>>;