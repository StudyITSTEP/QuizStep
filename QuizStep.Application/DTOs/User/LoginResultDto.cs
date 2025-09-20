using QuizStep.Core.Primitives;

namespace QuizStep.Application.DTOs.User;

public class LoginResultDto
{
    public Result? Result { get; set; }
    public string Token { get; set; }
}