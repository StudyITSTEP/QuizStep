using MediatR;
using QuizStep.Application.DTOs.User;

namespace QuizStep.Application.Commands___Queries.User;

public record RefreshTokenCommand(string RefreshToken, string UserId): IRequest<RefreshTokenDto?>;