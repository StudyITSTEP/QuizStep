using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.UserErrors;

public class LoginError
{
    public static Error UserOrPassword = new Error(nameof(UserOrPassword), "User or password is incorrect");
}