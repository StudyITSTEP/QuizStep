using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.UserErrors;

public static class RegisterError
{
    public static Error RegisterFailed = new(nameof(RegisterFailed));
}