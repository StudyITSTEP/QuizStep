using QuizStep.Core.Entities;

namespace QuizStep.Core.Interfaces;

public interface IJwtProvider
{
    public string GetJwt(User user);
    public string GenerateJwtToken(User user);
    public bool ValidateToken(string token);
}