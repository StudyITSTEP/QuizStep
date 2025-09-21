using QuizStep.Core.Primitives;

namespace QuizStep.Core.Errors.UserErrors;

public class RegisterError
{
    public static Error RegisterFailed = new(nameof(RegisterFailed));
}