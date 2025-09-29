using QuizStep.Core.Primitives;

namespace QuizStep.Application.DTOs.User;

public class LoginResultDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}